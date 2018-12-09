using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FireAdmin.Models
{
    public class StationType
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please enter a Station Type.")]
        [DisplayName("Station Type")]
        public string Type { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DeletedBy { get; set; }
        public DateTime DateDeleted { get; set; }
    }
}