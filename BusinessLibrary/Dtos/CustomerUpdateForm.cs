using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class CustomerUpdateForm
{
    [Required]
    public string Name { get; set; } = null!;
}


