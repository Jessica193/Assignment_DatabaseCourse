using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Index(nameof(Name), IsUnique = true)]
public class CustomerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    public ICollection<ContactPersonEntity> ContactPerson { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}
