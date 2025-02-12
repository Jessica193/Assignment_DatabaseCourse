using BusinessLibrary.Interfaces;
using BusinessLibrary.Services;
using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp.Dialogs;
using Presentation.ConsoleApp.Interfaces;
using System.Security.Authentication.ExtendedProtection;
using System.Text.Json;
using System.Text.Json.Serialization;

var options = new JsonSerializerOptions()
{
    WriteIndented = true,
    ReferenceHandler = ReferenceHandler.Preserve
};

var services = new ServiceCollection();

services.AddDbContext<DataContext>(options => options.UseSqlite("Data Source=my_database.db"));

services.AddScoped<IProjectRepository, ProjectRepository>();
services.AddScoped<ICustomerRepository, CustomerRepository>();
services.AddScoped<IContactPersonRepository, ContactPersonRepository>();
services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<IRoleRepository, RoleRepository>();
services.AddScoped<IServiceRepository, ServiceRepository>();
services.AddScoped<IUnitTypeRepository, UnitTypeRepository>();
services.AddScoped<IStatusTypeRepository, StatusTypeRepository>();

services.AddScoped<IProjectService, ProjectService>();
services.AddScoped<ICustomerService, CustomerService>();
services.AddScoped<IContactPersonService, ContactPersonService>();
services.AddScoped<IEmployeeService, EmployeeService>();
services.AddScoped<IRoleService, RoleService>();
services.AddScoped<IServiceService, ServiceService>();
services.AddScoped<IUnitTypeService, UnitTypeService>();
services.AddScoped<IStatusTypeService, StatusTypeService>();

services.AddScoped<IMenuDialogs, MenuDialogs>();

var serviceProvider = services.BuildServiceProvider();
var menudialog = serviceProvider.GetRequiredService<IMenuDialogs>();

menudialog.Run();