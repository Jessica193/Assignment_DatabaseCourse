using Azure;
using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using Data.Entities;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs(IProjectService projectService, ICustomerService customerService, IContactPersonService contactPersonService, IServiceService serviceService, IUnitTypeService unitTypeService, IEmployeeService employeeService, IRoleService roleService, IStatusTypeService statusTypeService, ICustomerDialogs customerDialogs, IContactPersonDialogs contactPersonDialogs, IEmployeeDialogs employeeDialogs, IRoleDialogs roleDialogs, IServiceDialogs serviceDialogs, IStatusTypeDialogs statusTypeDialogs, IUnitTypeDialogs unitTypeDialogs, IProjectDialogs projectDialogs) : IMenuDialogs
{
    private readonly IProjectService _projectService = projectService;
    private readonly ICustomerService _customerService = customerService;
    private readonly IContactPersonService _contactPersonService = contactPersonService;
    private readonly IServiceService _serviceService = serviceService;
    private readonly IUnitTypeService _unitTypeService = unitTypeService;
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IRoleService _roleService = roleService;
    private readonly IStatusTypeService _statusTypeService = statusTypeService;
    private readonly ICustomerDialogs _customerDialogs = customerDialogs;
    private readonly IContactPersonDialogs _contactPersonDialogs = contactPersonDialogs;
    private readonly IEmployeeDialogs _employeeDialogs = employeeDialogs;
    private readonly IRoleDialogs _roleDialogs = roleDialogs;
    private readonly IServiceDialogs _serviceDialogs = serviceDialogs;
    private readonly IStatusTypeDialogs _statusTypeDialogs = statusTypeDialogs;
    private readonly IUnitTypeDialogs _unitTypeDialogs = unitTypeDialogs;
    private readonly IProjectDialogs _projectDialogs = projectDialogs;

    public async Task RunAsync()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** MAIN MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Manage customers");
            Console.WriteLine("2. Manage contact persons");
            Console.WriteLine("3. Manage Employees");
            Console.WriteLine("4. Manage roles");
            Console.WriteLine("5. Manage Services");
            Console.WriteLine("6. Manage status types");
            Console.WriteLine("7. Manage unit types");
            Console.WriteLine("8. Manage Projects");
            Console.WriteLine("9. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await _customerDialogs.RunAsync();
                    break;
                case "2":
                    await _contactPersonDialogs.RunAsync();
                    break;
                case "3":
                    await _employeeDialogs.RunAsync();
                    break;

                case "4":
                    await _roleDialogs.RunAsync();
                    break;
                case "5":
                    await _serviceDialogs.RunAsync();
                    break;
                case "6":
                    await _statusTypeDialogs.RunAsync();
                    break;
                case "7":
                    await _unitTypeDialogs.RunAsync();
                    break;
                case "8":
                    await _projectDialogs.RunAsync();
                    break;
                case "9":
                     QuitApplication();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("You must enter a valid option, press any key to continue");
                    Console.ReadKey();
                    break;
            }

        }


    }




    //public void CreateProject2()
    //{
    //    var form = new ProjectRegistrationForm();

    //    Console.WriteLine("Name");
    //    form.Name = Console.ReadLine()!;


    // Välj status: 
    // statuses = service.getAllStatus()
    // foreach (var status in statuses)
    // console.writeline(status.id, status.name)
    // Console.WriteLine("ange status id").
    // form.statusId = Console.ReadLine()!


    // Console.WriteLine("ange customer id").
    // form.customerId = Console.ReadLine()!


    // projectService.Create(form)

    // projectservice



    // var customerResult = customerService.CreateCustomer(customerFactory(form.Customer))

    //var statusModel = statusService.CreateStatus(statusFactory(form.Status))

    // var serviceModel = serviceService.CreateService(serviceFactory(form.Service, form.Unit))

    //    var projectEntity = new ProjectEntity
    //    {
    //        Name = form.Name,
    //        Description = form.Description,
    //        StartDate = form.StartDate,
    //        EndDate = form.EndDate,
    //        QuantityofServiceUnits = form.QuantityofServiceUnits,
    //        TotalPrice = 100,
    //       // CustomerId = form.customerId
    //       //StatusTypeId = form.statusId
    //       //ServiceId = serviceModel.Id
    //    };



    //    // projectRepository.CreateProjectAsync(projectEntity)
    //}

    public async Task CreateProjectAsync()
    {
        var projectForm = ProjectFactory.Create();

        Console.Clear();
        Console.WriteLine("***** Information about the project *****");

        Console.Write("Project name: ");
        projectForm.Name = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Project description: ");
        projectForm.Description = Console.ReadLine()!;
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
        projectForm.StartDate = startDate;
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
        projectForm.EndDate = endDate;
        Console.Write("");
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
