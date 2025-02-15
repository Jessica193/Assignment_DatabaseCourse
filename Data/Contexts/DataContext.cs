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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Customer)
            .WithMany(c => c.Projects)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Employee)
            .WithMany(e => e.Projects)
            .HasForeignKey(p => p.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.Service)
            .WithMany(s => s.Projects)
            .HasForeignKey(p => p.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProjectEntity>()
            .HasOne(p => p.StatusType)
            .WithMany(st => st.Projects)
            .HasForeignKey(p => p.StatusTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ContactPersonEntity>()
            .HasOne(cp => cp.Customer)
            .WithMany(c => c.ContactPersons)
            .HasForeignKey(cp => cp.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<EmployeeEntity>()
            .HasOne(e => e.Role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ServiceEntity>()
           .HasOne(s => s.Unit)
           .WithMany(ut => ut.Services)
           .HasForeignKey(s => s.UnitTypeId)
           .OnDelete(DeleteBehavior.Restrict);



        base.OnModelCreating(modelBuilder);
    }
}
