using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class EmployeeUpdateForm
{

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
}


