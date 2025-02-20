using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Data.Entities;


namespace BusinessLibrary.Dtos
{
    public class ProjectRegistrationForm
    {
        [Required (ErrorMessage = "The name is required")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }


        [Required(ErrorMessage = "The start date is required")]
        public DateTime StartDate { get; set; }


        [Required(ErrorMessage = "The end date is required")]
        public DateTime EndDate { get; set; }


        [Required(ErrorMessage = "The quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int QuantityofServiceUnits { get; set; }


        [Required(ErrorMessage = "The customer is required")]
        public int CustomerId { get; set; }


        [Required(ErrorMessage = "The employee is required")]
        public int EmployeeId { get; set; }


        [Required(ErrorMessage = "The status type is required")]
        public int StatusTypeId { get; set; }


        [Required(ErrorMessage = "The service is required")]
        public int ServiceId { get; set; } 
    }
}
