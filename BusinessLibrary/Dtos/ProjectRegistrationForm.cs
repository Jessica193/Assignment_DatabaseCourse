using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Data.Entities;


namespace BusinessLibrary.Dtos
{
    public class ProjectRegistrationForm
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

       
        public DateTime StartDate { get; set; }

        
        public DateTime EndDate { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int QuantityofServiceUnits { get; set; }

       
        //public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }

        public int EmployeeId { get; set; }

        
        public int StatusTypeId { get; set; }

        public int ServiceId { get; set; }



       

        
        
        

        

       
    }
}
