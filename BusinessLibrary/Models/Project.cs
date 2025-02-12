using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models
{
    public class Project
    {
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int QuantityofServiceUnits { get; set; }
        public decimal TotalPrice { get; set; }




        public StatusType StatusType { get; set; } = null!;
        public Service Service { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
        public Customer Customer { get; set; } = null!;
    }
}
