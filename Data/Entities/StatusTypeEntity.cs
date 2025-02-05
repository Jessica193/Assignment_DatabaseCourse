using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(Status), IsUnique = true)]
public class StatusTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(20)")]
    public string Status { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];  
}


   