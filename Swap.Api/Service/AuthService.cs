using Microsoft.AspNetCore.Identity;
using Swap.Api.ValueObjects;
using System.Threading.Tasks;
using Swap.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Text;
using Microsoft.Extensions.Options;

namespace Swap.Api.Service
{
	public interface IAuthService
	{
		Task<IdentityResult> CreateAsync(RegisterUserVO registerUser);
		Task<ApplicationUser> GetUserAsync(string email);
		Task<string> GetJwt(string email);
	}


	public class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly AppSettings _appSettings;

		public AuthService(UserManager<ApplicationUser> userManager, IOptions<AppSettings> appSettings)
		{
			_appSettings = appSettings.Value;
			_userManager = userManager;
		}

		public async Task<IdentityResult> CreateAsync(RegisterUserVO registerUser)
		{
			var user = new ApplicationUser
			{
				UserName = registerUser.Email,
				Email = registerUser.Email,
				MobileNumber = registerUser.MobileNumber,
				EmailConfirmed = true
			};


			var result = await _userManager.CreateAsync(user, registerUser.Password);

			return result;
		}

		public async Task<ApplicationUser> GetUserAsync(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			return user;
		}

		public async Task<string> GetJwt(string email)
		{
			var user = await GetUserAsync(email);

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Name, user.UserName.ToString()),
					new Claim(ClaimTypes.Email, user.Email.ToString()),
					new Claim(ClaimTypes.MobilePhone, user.MobileNumber)
				}),
				Issuer = _appSettings.Issuer,
				Audience = _appSettings.Audience,
				Expires = DateTime.UtcNow.AddHours(_appSettings.Exp),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}
	}
}
