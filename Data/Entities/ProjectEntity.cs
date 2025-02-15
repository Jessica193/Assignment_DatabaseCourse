using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(100)")]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public DateTime StartDate {  get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }

    [Required]
    public int QuantityofServiceUnits { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }

    public int CustomerId {  get; set; }
    public int EmployeeId { get; set; }
    public int ServiceId { get; set; }
    public int StatusTypeId { get; set; }

    public StatusTypeEntity StatusType { get; set; } = null!;
    public ServiceEntity Service { get; set; } = null!;
    public EmployeeEntity Employee { get; set; } = null!;
    public CustomerEntity Customer { get; set; } = null!;



    //    🔹 Detta händer i databasen:

    //Endast CustomerId lagras i Projects-tabellen.
    //Ingen ny Customer skapas, utan den pekar på en existerande Customer i databasen.
    //Navigation property Customer kommer vara null om du hämtar ProjectEntity utan att inkludera Customer.
    //Om du vill att Customer, Service, Employee och StatusType automatiskt ska fyllas på i ProjectEntity, använd.Include():
}
