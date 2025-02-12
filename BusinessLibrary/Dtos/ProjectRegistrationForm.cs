using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using BusinessLibrary.Models;
using BusinessLibrary.Factories;

namespace BusinessLibrary.Dtos
{
    public class ProjectRegistrationForm
    {
        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int QuantityofServiceUnits { get; set; }

        //public Service Service { get; set; } = null!; //RÄTT??
    }
}
