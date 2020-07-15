using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Swap.Api.ValueObjects
{
	public class RegisterUserVO
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }

	}
	public class RegisterUserValidation : AbstractValidator<RegisterUserVO>
	{
		public RegisterUserValidation()
		{
			RuleFor(x => x.Email).NotNull().WithMessage("Email é obrigatório");
			RuleFor(x => x.Email).EmailAddress().WithMessage("Formato do e-mail inválido");
			RuleFor(x => x.Password).NotNull().WithMessage("Senha obrigatória");
			RuleFor(x => x.Password).MinimumLength(6);
			RuleFor(x => x.Password).MaximumLength(15);
			RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("As senhas não conferem");

				


		}
	}

	public class LoginUserVO
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}

	public class LoginUserValidation : AbstractValidator<LoginUserVO>
	{
		public LoginUserValidation()
		{
			RuleFor(x => x.Email).NotNull().WithMessage("Email é obrigatório");
			RuleFor(x => x.Email).EmailAddress().WithMessage("Formato do e-mail inválido");
			RuleFor(x => x.Password).NotNull().WithMessage("Senha obrigatória");
			RuleFor(x => x.Password).MinimumLength(6);
			RuleFor(x => x.Password).MaximumLength(15);
		}
	}

}
