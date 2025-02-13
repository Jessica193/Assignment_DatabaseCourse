using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class StatusTypeRegistrationForm
{
    [Required]
    public string Status { get; set; } = null!;

}
