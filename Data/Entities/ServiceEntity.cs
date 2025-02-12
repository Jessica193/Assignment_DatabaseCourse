using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class ServiceEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string Name { get; set; } = null!;

    [Required]
    public decimal PricePerUnit { get; set; }

    [Required]
    public int UnitTypeId { get; set; }

    public UnitTypeEntity Unit { get; set; } = null!;
}
