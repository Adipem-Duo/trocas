using Microsoft.AspNetCore.Identity;

namespace Swap.Api.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string MobileNumber { get; set; }

	}
}
