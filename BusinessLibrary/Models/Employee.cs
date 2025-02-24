﻿using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public ICollection<Project> Projects { get; set; } = null!;


}
