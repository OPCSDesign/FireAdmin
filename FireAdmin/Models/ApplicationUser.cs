using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace FireAdmin.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public string Email { get; set; }
	}
}