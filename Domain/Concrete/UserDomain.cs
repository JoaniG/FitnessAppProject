using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.UserDTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class UserDomain : DomainBase, IUserDomain
    {
        public UserDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        private IUserRepository userRepository => _unitOfWork.GetRepository<IUserRepository>();

        public UserDTO GetByUsername(string username)
        {
            User user = userRepository.GetByUsername(username);
            return _mapper.Map<UserDTO>(user);
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

        //public TokenDTO Login(LoginDTO loginDTO)
        //{
        //    var user = CheckUsername(loginDTO.Username);
        //    if (user == null)
        //        throw new ArgumentException("Invalid username");

        //    if (isPasswordCorrect(user, loginDTO.Password) == false)
        //        throw new ArgumentException("Invalid password");

        //    return new TokenDTO
        //    {
        //        Username = user.Username,
        //        Token = _tokenService.CreateToken(user)
        //    };
        //}

        private User CheckUsername(string username)
        {
            var user = userRepository.GetByUsername(username);
            return user;
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = userRepository.GetAll().AsEnumerable();
            List<UserDTO> userList = new List<UserDTO>();
            foreach(var user in users)
            {
                var listUser = _mapper.Map<UserDTO>(user);
                userList.Add(listUser);
            }
            return userList;
        }

        public UserDTO GetUserById(Guid id)
        {
            var user = userRepository.GetById(id);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
