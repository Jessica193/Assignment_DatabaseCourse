using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models
{
    public class ProjectRegistrationForm
    {

        [Required]
        [MinLength(2, ErrorMessage = "Name must be at least two characters long.")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        
    }
}
