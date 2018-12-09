using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FireAdmin.Models
{
	public class Brigade
	{
		[Required]
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Please enter a Brigade Name.")]
		[DisplayName("Brigade")]
		public string Name { get; set; }
		public bool IsDeleted { get; set; }
		public Guid DeletedBy { get; set; }
		public DateTime DateDeleted { get; set; }
	}
}