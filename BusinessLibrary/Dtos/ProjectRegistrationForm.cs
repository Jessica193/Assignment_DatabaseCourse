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

       
        public decimal TotalPrice { get; set; }

        public int CustomerId { get; set; }

       // public CustomerRegistrationForm Customer { get; set; } = null!;
        public int EmployeeId { get; set; }

        //public EmployeeRegistrationForm Employee { get; set; } = null!;
        
        public int StatusTypeId { get; set; }

        //public StatusTypeRegistrationForm Status { get; set; } = null!;

        public int ServiceId { get; set; }

        //public ServiceRegistrationForm Service { get; set; } = null!;

     
       // public ContactPersonRegistrationForm ContactPerson { get; set; } = null!;


       

        
        
        

        

       
    }
}
