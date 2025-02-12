using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class RoleUpdateForm
{
    [Required]
    public string Name { get; set; } = null!;
}


