using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class ServiceUpdateForm
{
    [Required]
    public string Name { get; set; } = null!;

    [Required]
    public decimal PricePerUnit { get; set; }
}


