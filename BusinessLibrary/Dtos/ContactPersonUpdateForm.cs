using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class ContactPersonUpdateForm
{

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;


}


