using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
        public decimal TotalPrice { get; set; }  //ska vara en beräkning av service.PricePerUnit * service.Quantity eller 
                                                    //serviceEntity.PricePerUnit * serviceEntity.Quantity. Nåt sånt.



    }
}
