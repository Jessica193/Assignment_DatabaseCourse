using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class ServiceRegistrationForm
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price per unit must be greater than zero")]
    public decimal PricePerUnit { get; set; }

    public int UnitTypeId { get; set; }
    //public UnitTypeRegistrationForm Unit { get; set; } = null!;


}
