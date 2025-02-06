using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class ServiceRegistrationForm
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public decimal PricePerUnit { get; set; }


}
