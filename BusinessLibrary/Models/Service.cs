using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal PricePerUnit { get; set; }
    public int UnitTypeId { get; set; }
 
    public ICollection<Project> Projects { get; set; } = null!;

    public UnitType Unit { get; set; } = null!;

    //public string UnitType { get; set; } = null!;
}
