using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<UnitTypeEntity> UnitTypes { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<EmployeeEntity> Employee { get; set; }
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ContactPersonEntity> ContactPersons { get; set; }

}
