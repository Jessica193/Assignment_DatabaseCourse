using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Data.Contexts;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data Source=my_database.db");

        return new DataContext(optionsBuilder.Options);
    }
}
