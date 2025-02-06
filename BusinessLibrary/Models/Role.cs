using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
