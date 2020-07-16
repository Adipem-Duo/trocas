using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swap.Api.Models;
using Swap.Api.Service;
using Swap.Api.ValueObjects;

namespace Swap.Api.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly SignInManager<ApplicationUser> _signInMaganer;
		private readonly IAuthService _authService;

		public AuthController(
			IAuthService authService,
			SignInManager<ApplicationUser> signInMaganer
			)
		{
			_signInMaganer = signInMaganer;
			_authService = authService;
		}


		[HttpPost("register")]
		public async Task<ActionResult> Register(RegisterUserVO model)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

			var result = await _authService.CreateAsync(model);

			if (!result.Succeeded) return BadRequest(result.Errors);

			var user = await _authService.GetUserAsync(model.Email);

			await _signInMaganer.SignInAsync(user, false);
			return Ok(await _authService.GetJwt(user.Email));
		}

		[HttpPost("login")]
		public async Task<ActionResult> Login(LoginUserVO loginUser)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

			var result = await _signInMaganer.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);

			if (result.Succeeded) return Ok(await _authService.GetJwt(loginUser.Email));


			return BadRequest("usuário ou senha invalidos");
		}
	}
}