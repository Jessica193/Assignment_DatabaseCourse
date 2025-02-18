using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class ProjectDialogs(IProjectService projectService, ICustomerService customerService, IEmployeeService employeeService, IServiceService serviceService, IStatusTypeService statusTypeService) : IProjectDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IServiceService _serviceService = serviceService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** PROJECT MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a project");
            Console.WriteLine("2. View all projects");
            Console.WriteLine("3. View one project");
            Console.WriteLine("4. Update an project");
            Console.WriteLine("5. Delete an project");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateProjectAsync();
                    break;
                case "2":
                    await ViewAllProjectsAsync();
                    break;
                case "3":
                    await ViewOneProjectAsync();
                    break;
                case "4":
                    await UpdateProjectAsync();
                    break;
                case "5":
                    await DeleteProjectAsync();
                    break;
                case "6":
                    return;
                case "7":
                    QuitApplication();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("You must enter a valid option");
                    Console.ReadKey();
                    break;
            }
        }


    }
    public async Task CreateProjectAsync()
    {
        Console.Clear();
        var projectRegistrationForm = ProjectFactory.Create();

        Console.Clear();
        Console.WriteLine("***** Information about the project *****");

        Console.Write("Project name: ");
        projectRegistrationForm.Name = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Project description: ");
        projectRegistrationForm.Description = Console.ReadLine()!;
        Console.Write("");

        //Kod som konverterar string till datatypen DateTime är genererad av chatGPT4o
        DateTime startDate;
        while (true)
        {
            Console.Write("Project start date (yyyy-MM-dd): ");
            string? inputStartDate = Console.ReadLine();

            if (DateTime.TryParseExact(inputStartDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Unvalid format! Please use the format yyyy-MM-dd.");
            }
        }
        projectRegistrationForm.StartDate = startDate;
        Console.Write("");


        DateTime endDate;
        while (true)
        {
            Console.Write("Project end date (yyyy-MM-dd): ");
            string? inputEndDate = Console.ReadLine();

            if (DateTime.TryParseExact(inputEndDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Unvalid format! Please use the format yyyy-MM-dd.");
            }
        }
        projectRegistrationForm.EndDate = endDate;


        Console.Clear();
        Console.WriteLine("***** LIST OF SERVICES *****");
        Console.WriteLine("");
        var services = await _serviceService.GetAllServicesWithUnitTypeAsync();

        if (services.Any())
        {
            foreach (var service in services)
            { Console.WriteLine($"ID: {service.Id}, Name: {service.Name}, Price/unit: {service.PricePerUnit}, Unit: {service.Unit.Unit}"); }
        }
        else
        {
            Console.WriteLine("No services found");
        }
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");

        int service_Id;
        Console.WriteLine("Select service for the project (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out service_Id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        projectRegistrationForm.ServiceId = service_Id;


        int quantity;
        Console.WriteLine("Quantity: ");
        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.Write("Invalid input! Please enter a number: ");
        }

        projectRegistrationForm.QuantityofServiceUnits = quantity;


        Console.Clear();
        Console.WriteLine("***** LIST OF EMPLOYEES *****");
        Console.WriteLine("");
        var employees = await _employeeService.GetAllEmployeesWithRoleAsync();

        if (employees.Any())
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.Id}, Name: {employee.FirstName} {employee.LastName}, Email: {employee.Email}, Role: {employee.Role.Name}");
            }
        }
        else
        {
            Console.WriteLine("No employees found");
        }
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");

        int employee_Id;
        Console.WriteLine("Select employee for the project (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out employee_Id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        projectRegistrationForm.EmployeeId = employee_Id;



        Console.Clear();
        Console.WriteLine("***** LIST OF CUSTOMERS *****");
        Console.WriteLine("");
        var customers = await _customerService.GetAllCustomerAsync();

        if (customers.Any())
        {
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name} ");
            }
        }
        else
        {
            Console.WriteLine("No customer found");
        }
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");

        int customer_Id;
        Console.WriteLine("Select customer for the project (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out customer_Id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        projectRegistrationForm.CustomerId = customer_Id;



        Console.Clear();
        Console.WriteLine("***** LIST OF STATUS TYPES *****");
        Console.WriteLine("");
        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();

        if (statusTypes.Any())
        {
            foreach (var statusType in statusTypes)
            {
                Console.WriteLine($"ID: {statusType.Id}, Status: {statusType.Status} ");
            }
        }
        else
        {
            Console.WriteLine("No status types found");
        }
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");

        int status_Id;
        Console.WriteLine("Select status for the project (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out status_Id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        projectRegistrationForm.StatusTypeId = status_Id;


        var result = await _projectService.CreateAsync(projectRegistrationForm);
        if (result)
        {
            Console.WriteLine("Project was successfully created");
        }
        else
        {
            Console.WriteLine("Project was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewAllProjectsAsync()
    {
        Console.Clear();
        var projects = await _projectService.GetAllProjectsWithDetailsAsync();

        Console.WriteLine("***** LIST OF PROJECTS *****");

        if (projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine($"PROJECT: {project.Id} ");
                Console.WriteLine($"Name: { project.Name}");
                Console.WriteLine($"Description: {project.Description}");
                Console.WriteLine($"Start date: {project.StartDate}");
                Console.WriteLine($"End date: {project.EndDate}");
                Console.WriteLine($"Status: {project.StatusType.Status}");
                Console.WriteLine("");
                Console.WriteLine("SERVICE INFO");
                Console.WriteLine($"Service: {project.Service.Name}");
                Console.WriteLine($"Price/unit: {project.Service.PricePerUnit}");
                Console.WriteLine($"Unit: {project.Service.Unit.Unit}");
                Console.WriteLine($"Quantity: {project.QuantityofServiceUnits}");
                Console.WriteLine($"Total price: {project.TotalPrice}");
                Console.WriteLine("");
                Console.WriteLine("CUSTOMER INFO");
                Console.WriteLine($"Customer: {project.Customer.Name}");
                Console.WriteLine($"Contact persons: ");
                foreach (var contactPerson in project.Customer.ContactPersons)
                {
                    Console.WriteLine($"Name: {contactPerson.FirstName} {contactPerson.LastName}, Email: {contactPerson.Email}, Phone number: {contactPerson.PhoneNumber}");
                }
                Console.WriteLine("");
                Console.WriteLine("EMPLOYEE INFO");
                Console.WriteLine($"Name: {project.Employee.FirstName} {project.Employee.LastName}");
                Console.WriteLine($"Email: {project.Employee.Email}");
                Console.WriteLine($"Role: {project.Employee.Role.Name}");
                Console.WriteLine("---------------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("No projects found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneProjectAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the project you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var project = await _projectService.GetProjectWithDetailsByIdAsync(id);
        if (project != null)
        {
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"PROJECT: {project.Id} ");
            Console.WriteLine($"Name: {project.Name}");
            Console.WriteLine($"Description: {project.Description}");
            Console.WriteLine($"Start date: {project.StartDate}");
            Console.WriteLine($"End date: {project.EndDate}");
            Console.WriteLine($"Status: {project.StatusType.Status}");
            Console.WriteLine("");
            Console.WriteLine("SERVICE INFO");
            Console.WriteLine($"Service: {project.Service.Name}");
            Console.WriteLine($"Price/unit: {project.Service.PricePerUnit}");
            Console.WriteLine($"Unit: {project.Service.Unit.Unit}");
            Console.WriteLine($"Quantity: {project.QuantityofServiceUnits}");
            Console.WriteLine($"Total price: {project.TotalPrice}");
            Console.WriteLine("");
            Console.WriteLine("CUSTOMER INFO");
            Console.WriteLine($"Customer: {project.Customer.Name}");
            Console.WriteLine($"Contact persons: ");
            foreach (var contactPerson in project.Customer.ContactPersons)
            {
                Console.WriteLine($"Name: {contactPerson.FirstName} {contactPerson.LastName}, Email: {contactPerson.Email}, Phone number: {contactPerson.PhoneNumber}");
            }
            Console.WriteLine("");
            Console.WriteLine("EMPLOYEE INFO");
            Console.WriteLine($"Name: {project.Employee.FirstName} {project.Employee.LastName}");
            Console.WriteLine($"Email: {project.Employee.Email}");
            Console.WriteLine($"Role: {project.Employee.Role.Name}");
            Console.WriteLine("---------------------------------------------");
        }
        else
        {
            Console.WriteLine("Project was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }


    public async Task UpdateProjectAsync()
    {
        Console.Clear();

        Console.WriteLine("***** LIST OF PROJECTS *****");
        var projects = await _projectService.GetAllProjectsAsync();

        if (projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Name: {project.Name}, Start date: {project.StartDate}, End date: {project.EndDate}, Status: {project.StatusType.Status}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No projects found");
        }
        Console.WriteLine("---------------------------------------");

        int id;
        Console.WriteLine("Enter the ID-number for the project you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }


        var projectUpdateForm = new ProjectUpdateForm();
        Console.Write($"Change name to: ");
        projectUpdateForm.Name = Console.ReadLine()!.Trim();

        Console.Write($"Change description to: ");
        projectUpdateForm.Description = Console.ReadLine()!.Trim();

        DateTime startDate;
        while (true)
        {
            Console.Write("Change start date to (yyyy-MM-dd): ");
            string? inputStartDate = Console.ReadLine();

            if (DateTime.TryParseExact(inputStartDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Unvalid format! Please use the format yyyy-MM-dd.");
            }
        }
        projectUpdateForm.StartDate = startDate;

        DateTime endDate;
        while (true)
        {
            Console.Write("Change end date to (yyyy-MM-dd): ");
            string? inputEndDate = Console.ReadLine();

            if (DateTime.TryParseExact(inputEndDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
                break;
            }
            else
            {
                Console.WriteLine("Unvalid format! Please use the format yyyy-MM-dd.");
            }
        }
        projectUpdateForm.EndDate = endDate;

        int quantity;
        Console.WriteLine("Quantity: ");
        while (!int.TryParse(Console.ReadLine(), out quantity))
        {
            Console.Write("Invalid input! Please enter a number: ");
        }
        projectUpdateForm.QuantityofServiceUnits = quantity;



        var result = await _projectService.UpdateProjectAsync(id, projectUpdateForm);
        if (result)
        {
            Console.WriteLine("Project was successfully updated");
        }
        else
        {
            Console.WriteLine("Project was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteProjectAsync()
    {
        Console.Clear();

        Console.WriteLine("***** LIST OF PROJECTS *****");
        var projects = await _projectService.GetAllProjectsAsync();

        if (projects.Any())
        {
            foreach (var project in projects)
            {
                Console.WriteLine($"ID: {project.Id}, Name: {project.Name}, Start date: {project.StartDate}, End date: {project.EndDate}, Status: {project.StatusType.Status}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No projects found");
        }
        Console.WriteLine("---------------------------------------");


        int id;
        Console.WriteLine("Enter the ID-number for the project you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _projectService.DeleteProjectAsync(id);
        if (result)
        {
            Console.WriteLine("Project was successfully deleted");
        }
        else
        {
            Console.WriteLine("Project was not deleted");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public void QuitApplication()
    {
        {
            Console.Clear();
            Console.Write("Do you want to qiut this application (y/n): ");
            string answer = Console.ReadLine()!.ToLower().Trim();

            if (string.IsNullOrEmpty(answer))
            {
                Console.WriteLine("You must enter a valid option");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else if (answer == "y")
            {
                Environment.Exit(0);
            }
        }
    }
}
