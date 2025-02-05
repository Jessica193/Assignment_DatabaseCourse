using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Index(nameof(Email), IsUnique = true)]
public class ContactPersonEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName ="nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(150)")]
    public string Email { get; set; } = null!;

    [Required]
    [Column(TypeName = "varchar(30)")]
    public string PhoneNumber { get; set; } = null!;

    public int CustomerId { get; set; }

    [ForeignKey("CustomerId")]
    public CustomerEntity Customer { get; set; } = null!;
}
