using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swap.Api.ValueObjects;

namespace Swap.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
		private readonly SignInManager<IdentityUser> _signInMaganer;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly AppSettings _appSettings;

		public AuthController(
			SignInManager<IdentityUser> signInMaganer,
			UserManager<IdentityUser> userManager,
			IOptions<AppSettings> appSettings
			)
		{
			_signInMaganer = signInMaganer;
			_userManager = userManager;
			_appSettings = appSettings.Value;
		}


		[HttpPost("register")]
		public async Task<ActionResult> Register(RegisterUserVO model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

			var user = new IdentityUser
			{
				UserName = model.Email,
				Email = model.Email,
				EmailConfirmed = true
			};
			var result = await _userManager.CreateAsync(user, model.Password);
			if (!result.Succeeded) return BadRequest(result.Errors);


			await _signInMaganer.SignInAsync(user, false);
			return Ok(await GerarJwt(user.Email));
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginUserVO loginUser)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

			var result = await _signInMaganer.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);
			
			if (result.Succeeded) return Ok(await GerarJwt(loginUser.Email));


			return BadRequest("usuário ou senha invalidos");
		}


		private async Task<string> GerarJwt(string email)
		{
			var user = await _userManager.FindByEmailAsync(email);

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = _appSettings.Issuer,
				Audience = _appSettings.Audience,
				Expires = DateTime.UtcNow.AddHours(_appSettings.Exp),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
		}
	}
}