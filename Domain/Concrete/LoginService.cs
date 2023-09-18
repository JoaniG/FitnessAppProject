using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class LoginService : DomainBase, ILoginService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginService(IConfiguration configuration, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IMapper mapper, IUnitOfWork unitOfWork, 
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
           _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<TokenDTO> Login(LoginDTO credentials)
        {
            var user = CheckUsername(credentials.Username);
            if (user == null)
                throw new ArgumentException("Invalid username");

            if (isPasswordCorrect(user, credentials.Password) == false)
                throw new ArgumentException("Invalid password");

            // Generate access token
            var accessToken = GenerateAccessToken(user);

            // Generate refresh token
            var refreshTokenValue = GenerateRefreshToken();

            RefreshToken refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                IssuedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(60),
                Revoked = false,
                UserId = user.Id

            };
            // Store refresh token in the database
             _refreshTokenRepository.Add(refreshToken);
            _unitOfWork.Save();

            // Return token response
            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }

        private User CheckUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            return user;
        }

        private bool isPasswordCorrect(User user, string password)
        {
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return false;
            }
            return true;
        }

        public async Task<TokenDTO> RefreshTokenAsync(string refreshToken)
        {
            // Retrieve the refresh token from the database
            var tokenData = _refreshTokenRepository.GetByValue(refreshToken);
            if (tokenData == null || tokenData.ExpirationDate < DateTime.UtcNow)
            {
                return null; // Invalid refresh token or expired
            }

            // Retrieve the user associated with the refresh token
            var user = _userRepository.GetById(Guid.Empty);//tokenData.UserId);
            if (user == null)
            {
                return null; // User not found
            }

            // Generate a new access token
            var accessToken = GenerateAccessToken(user);

            RefreshToken tokenToUpdate = _refreshTokenRepository.GetByValue(refreshToken);
            tokenToUpdate.ExpirationDate = tokenToUpdate.ExpirationDate.AddDays(60);

            // Update the refresh token's expiration date in the database
            _refreshTokenRepository.Update(tokenToUpdate);
            _unitOfWork.Save();

            // Return token response
            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        public void RevokeRefreshTokenAsync(string refreshToken)
        {
            // Revoke the refresh token by deleting it from the database
            RefreshToken token = _refreshTokenRepository.GetByValue(refreshToken);
            _refreshTokenRepository.Remove(token);
            _unitOfWork.Save();
        }

        private string GenerateAccessToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["SecretKey"]);
            string id = Convert.ToString(user.Id);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier as String, id as String)
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var refreshToken = Guid.NewGuid().ToString();
            return refreshToken;
        }

        public TokenDTO Register(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();

            User user = new User
            {
                Id = Guid.NewGuid(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key,
                Email = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Username = registerDTO.Username,
                Status = true,
                WeightSetting = true,
                HeightSetting = true
                
            };

            _userRepository.Add(user);
            _unitOfWork.Save();

            var accessToken = GenerateAccessToken(user);
            var refreshTokenValue = GenerateRefreshToken();
            RefreshToken refreshToken = new RefreshToken
            {
                Token = refreshTokenValue,
                IssuedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(60),
                Revoked = false,
                UserId = user.Id

            };
            _refreshTokenRepository.Add(refreshToken);
            _unitOfWork.Save();

            return new TokenDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenValue
            };
        }

        public void ChangePassword(PasswordChangeDTO passwordChangeDTO)
        {
            var user = _userRepository.GetById(passwordChangeDTO.UserId);

            if (passwordChangeDTO.NewPassword != passwordChangeDTO.ConfirmNewPassword)
                throw new ArgumentException("Passwords do not match.");

            if (isPasswordCorrect(user, passwordChangeDTO.OldPassword) == false)
                throw new ArgumentException("Old password is not correct.");

            _ = CheckPassword(passwordChangeDTO.NewPassword);

            using var hmac = new HMACSHA512();

            // encrypt new one and update
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordChangeDTO.NewPassword));
            user.PasswordSalt = hmac.Key;

            _userRepository.Update(user);
            _unitOfWork.Save();
        }

        public bool CheckPassword(string password)
        {
            // password must be more than 8 chrs, at least one uppercase, at least one nr
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if (hasNumber.IsMatch(password) == false)
                throw new ArgumentException("Please include a number");
            if (hasUpperChar.IsMatch(password) == false)
                throw new ArgumentException("Please include an uppercase letter");
            if (hasMinimum8Chars.IsMatch(password) == false)
                throw new ArgumentException("Password must be 8 characters long");

            return true;
        }
    }

}
