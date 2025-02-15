using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Index(nameof(Unit), IsUnique = true)]
public class UnitTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string Unit { get; set; } = null!;

    public ICollection<ServiceEntity> Services { get; set; } = [];
}
