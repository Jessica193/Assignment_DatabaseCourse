using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class StatusType
{
    public int Id { get; set; }
    public string Status { get; set; } = null!;
}
