using Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace BusinessLibrary.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;






    public ICollection<ContactPerson> ContactPerson { get; set; } = null!;

    public ICollection<Project> Projects { get; set; } = [];
}
