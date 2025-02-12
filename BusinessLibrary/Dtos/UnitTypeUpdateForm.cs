using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class UnitTypeUpdateForm
{
    [Required]
    public string Unit { get; set; } = null!;
}
