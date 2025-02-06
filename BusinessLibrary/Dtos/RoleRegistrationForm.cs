using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class RoleRegistrationForm
{

    [Required]
    public string Name { get; set; } = null!;
}
