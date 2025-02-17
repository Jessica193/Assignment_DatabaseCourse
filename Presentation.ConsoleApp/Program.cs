using BusinessLibrary.Factories;
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


//Detta måste användas någonstans, var? kolla om video!
var options = new JsonSerializerOptions()
{
    WriteIndented = true,
    ReferenceHandler = ReferenceHandler.Preserve
};
//

var services = new ServiceCollection();

services.AddDbContext<DataContext>(dbOptions => dbOptions.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Projects_VSStudio\01.DatabaseCourse\Assignment_DatabaseCourse\Data\Databases\localDatabase.mdf;Integrated Security=True;Connect Timeout=30"));

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
services.AddScoped<ICustomerDialogs, CustomerDialogs>();
services.AddScoped<IContactPersonDialogs, ContactPersonDialogs>();
services.AddScoped<IEmployeeDialogs, EmployeeDialogs>();
services.AddScoped<IRoleDialogs, RoleDialogs>();
services.AddScoped<IServiceDialogs, ServiceDialogs>();
services.AddScoped<IStatusTypeDialogs, StatusTypeDialogs>();
services.AddScoped<IUnitTypeDialogs, UnitTypeDialogs>();
services.AddScoped<IProjectDialogs, ProjectDialogs>();

//services.AddScoped(provider => new Lazy<IMenuDialogs>(provider.GetRequiredService<IMenuDialogs>));

var serviceProvider = services.BuildServiceProvider();
var menudialog = serviceProvider.GetRequiredService<IMenuDialogs>();

await menudialog.RunAsync();






