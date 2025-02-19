using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Tests.Repositories;

public class ProjectRepository_Tests
{
    /// <summary>
    ///Eftersom BaseRepository är abstract och inte går att instansiera testar jag istället IProjectrepository som innehåller exakt
    //samma funktionalitet som min base samt alla andra repositories. 
    /// </summary>
    private DataContext _context;
    private IProjectRepository _projectRepository; 
   

    public ProjectRepository_Tests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid}")
            .Options;

        _context = new DataContext(options);
        _projectRepository = new ProjectRepository(_context);
       
    }

    [Fact]
    public async Task CreateAsync_ShouldReturnTrue_WhenEntityIsValid()
    {
        //Arrange
        var entity = new ProjectEntity() 
        { 
            Id = 1, 
            Name = "TestProject", 
            StartDate = DateTime.Now, 
            EndDate = DateTime.Now, 
            QuantityofServiceUnits = 4,
            CustomerId = 1,
            ServiceId = 1,  
            StatusTypeId = 1,
            EmployeeId = 1,
        };

        //Act
        var result = await _projectRepository.CreateAsync(entity);

        //Assert
        Assert.True(result);
        Assert.Equal("TestProject", entity.Name);
    }



    [Fact]
    public async Task GetAllAsync_ShouldReturnListOfEntities()
    {
        //Arrange

        //Act
        var result = await _projectRepository.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProjectEntity>>(result);
    }

    [Fact]
    public async Task GetAllWithDetailsAsync_ShouldReturnListOfEntitiesWithDetails()
    {
        //Arrange
        var contactperson = new ContactPersonEntity() { Id = 3, CustomerId = 3, FirstName = "Jenny", LastName = "Eriksson", Email = "jenny@domain.com", PhoneNumber = "0707555555" };
        _context.Add(contactperson);
        await _context.SaveChangesAsync();


        var customer = new CustomerEntity() { Id = 3, Name = "Testbolaget3 AB" };
        _context.Add(customer);
        await _context.SaveChangesAsync();


        var entity = new ProjectEntity()
        {
            Id = 4,
            Name = "TestProject 2",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 3,
            ServiceId = 3,
            StatusTypeId = 3,
            EmployeeId = 3,
        };
        _context.Add(entity);
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectRepository.GetAllWithDetailsAsync(query => query
        .Include(p => p.Customer)
        .ThenInclude(c => c.ContactPersons));

        //Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ProjectEntity>>(result);
        Assert.Equal("TestProject 2", result.FirstOrDefault(x => x.Id == entity.Id)!.Name);
        Assert.Equal("Testbolaget3 AB", result.FirstOrDefault(x => x.Id == 4)!.Customer.Name);
        Assert.IsAssignableFrom<IEnumerable<ContactPersonEntity>>(result.First().Customer.ContactPersons);
    }

    [Fact]
    public async Task GetOneAsync_ShouldReturnEntity_WhenExpressionNotNull()
    {
        //Arrange
        var entity = new ProjectEntity()
        {
            Id = 2,
            Name = "TestProject",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 1,
            ServiceId = 1,
            StatusTypeId = 1,
            EmployeeId = 1,
        };

        _context.Add(entity);
        await _context.SaveChangesAsync();  

        //Act
        var result = await _projectRepository.GetOneAsync(x => x.Id == 2);

        //Assert
        Assert.Equal(2, result.Id);
        Assert.NotNull(result);
        Assert.Equal("TestProject", entity.Name);
    }

    [Fact]
    public async Task GetOneWithDetailsAsync_ShouldReturnEntityWithDetails_WhenExpressionNotNull()
    {
        //Arrange

        var contactperson = new ContactPersonEntity() { Id = 1, CustomerId = 1, FirstName = "Jessica", LastName = "Eriksson", Email = "jessica@domain.com", PhoneNumber = "0707555555" };
        _context.Add(contactperson);
        await _context.SaveChangesAsync();


        var customer = new CustomerEntity() { Id = 1, Name = "Testbolaget AB" };
        _context.Add(customer);
        await _context.SaveChangesAsync();


        var entity = new ProjectEntity()
        {
            Id = 1,
            Name = "TestProject",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 1,
            ServiceId = 1,
            StatusTypeId = 1,
            EmployeeId = 1,
        };
        _context.Add(entity);
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectRepository.GetOneWithDetailsAsync(query => query
        .Include(p => p.Customer)
        .ThenInclude(c => c.ContactPersons)
        , x => x.Id == 1);

        //Assert
        Assert.Equal(1, result.Id);
        Assert.NotNull(result);
        Assert.Equal("TestProject", entity.Name);
        Assert.Equal("Testbolaget AB", result.Customer.Name);
        Assert.IsAssignableFrom<IEnumerable<ContactPersonEntity>>(result.Customer.ContactPersons);
    }


    [Fact]
    public async Task Update_ShouldReturnTrue_WhenEntityIsValid()
    {
        //Arrange
        var entity = new ProjectEntity()
        {
            Id = 5,
            Name = "TestProject 5",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 5,
            ServiceId = 5,
            StatusTypeId = 5,
            EmployeeId = 5,
        };
        _context.Add(entity);
        await _context.SaveChangesAsync();

        entity.Name = "Updated Project"; //Modifierar entiteten

        //Act
        var result = _projectRepository.Update(entity);

        //Assert
        Assert.True(result);
        Assert.Equal(EntityState.Modified, _context.Entry(entity).State); //Testar om entiteten markeras som Modified
    }


    [Fact]
    public async Task Delete_ShouldReturnTrue_WhenEntityExists()
    {
        //Arrange
        var entity = new ProjectEntity()
        {
            Id = 6,
            Name = "TestProject 6",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 6,
            ServiceId = 6,
            StatusTypeId = 6,
            EmployeeId = 6,
        };
        _context.Add(entity);
        await _context.SaveChangesAsync();

        //Act
        var result = _projectRepository.Delete(entity);

        //Assert
        Assert.True(result);
        Assert.Equal(EntityState.Deleted, _context.Entry(entity).State); 
    }




    [Fact]
    public async Task ExistsAsync_ShouldReturnTrue_WhenEntityExists()
    {
        //Arrange
        var entity = new ProjectEntity()
        {
            Id = 7,
            Name = "TestProject 7",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 7,
            ServiceId = 7,
            StatusTypeId = 7,
            EmployeeId = 7,
        };
        _context.Add(entity);
        await _context.SaveChangesAsync();

        //Act
        var result = await _projectRepository.ExistsAsync(x => x.Id == 7);

        //Assert
        Assert.True(result);
    }


    [Fact]
    public async Task SaveToDatabaseAsync_ShouldSaveChangesToDatabase()
    {
        //Arrange
        var entity = new ProjectEntity()
        {
            Id = 8,
            Name = "TestProject 8",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now,
            QuantityofServiceUnits = 4,
            CustomerId = 8,
            ServiceId = 8,
            StatusTypeId = 8,
            EmployeeId = 8,
        };
        _context.Add(entity);

        //Act
        await _projectRepository.SaveToDatabaseAsync();

        //Assert
        var savedProject = await _context.Projects.FindAsync(8);
        Assert.NotNull(savedProject);
        Assert.Equal(8, savedProject.Id);
    }







}
