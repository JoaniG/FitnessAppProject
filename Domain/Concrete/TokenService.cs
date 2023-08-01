using Domain.Contracts;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace Domain.Concrete
{
	public class TokenService : ITokenService
	{
		private readonly SymmetricSecurityKey _key;

		public TokenService(IConfiguration config)
		{
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
			//_roliDomain = roliDomain;
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim> {
				new Claim("UserId", user.Id.ToString())
			};

			var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDesc = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDesc);

			return tokenHandler.WriteToken(token);
		}
	}
}
