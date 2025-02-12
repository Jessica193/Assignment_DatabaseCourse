﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class Service
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal PricePerUnit { get; set; }
    public UnitType Unit { get; set; } = null!;
}
