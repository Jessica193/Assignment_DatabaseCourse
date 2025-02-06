using Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class CustomerRegistrationForm
{

    [Required]
    public string Name { get; set; } = null!;

}
