﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Dtos;

public class ProjectUpdateForm
{
    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int QuantityofServiceUnits { get; set; }

    public decimal TotalPrice { get; set; }

    //public Service Service { get; set; } = null!; //RÄTT??
}
