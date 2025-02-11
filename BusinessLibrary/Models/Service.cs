using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal PricePerUnit { get; set; }
    public UnitType Unit { get; set; } = null!;
}
