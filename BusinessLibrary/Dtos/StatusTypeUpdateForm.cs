using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class StatusTypeUpdateForm
{
    [Required]
    public string Status { get; set; } = null!;
}


