using Azure;
using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using Data.Entities;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs(IProjectService projectService, ICustomerService customerService, IContactPersonService contactPersonService, IServiceService serviceService, IUnitTypeService unitTypeService, IEmployeeService employeeService, IRoleService roleService, IStatusTypeService statusTypeService, ICustomerDialogs customerDialogs, IContactPersonDialogs contactPersonDialogs) : IMenuDialogs
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



    //public void Run()
    //{
    //    while (true)
    //    {
    //        Console.Clear();
    //        var option = MainMenu();
    //        if (!string.IsNullOrEmpty(option))
    //        {
    //            OptionSwitch(option);
    //        }
    //        else
    //        {
    //            Console.Clear();
    //            Console.WriteLine("You must enter an option");
    //        }
    //    }
    //}

    public async Task RunAsync()
    {

        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** MAIN MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Manage customers");
            Console.WriteLine("2. Manage customer contact persons");
            Console.WriteLine("3. Manage Employees");
            Console.WriteLine("4. Manage employee roles");
            Console.WriteLine("5. Manage Services");
            Console.WriteLine("6. Manage status types");
            Console.WriteLine("7. Manage service unit types");
            Console.WriteLine("8. Manage Projects");
            Console.WriteLine("9. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;
            //return option;
            switch (option)
            {
                case "1":
                    await _customerDialogs.RunAsync();
                    break;
                case "2":
                    await _contactPersonDialogs.RunAsync();
                    break;
                case "3":
                    await ViewOneProject();
                    break;
                case "4":
                    await UpdateProject();
                    break;
                case "5":
                    await DeleteProject();
                    break;
                case "6":
                    await QuitApplication();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("You must enter a valid option, press any key to continue");
                    Console.ReadKey();
                    break;
            }

        }
        
      
    }

    //public void OptionSwitch(string option)
    //{
    //    switch (option)
    //    {
    //        case "1":
    //            _customerDialogs.Run();
    //            break;
    //        case "2":
    //            ViewAllProjects();
    //            break;
    //        case "3":
    //            ViewOneProject();
    //            break;
    //        case "4":
    //            UpdateProject();
    //            break;
    //        case "5":
    //            DeleteProject();
    //            break;
    //        case "6":
    //            QuitApplication();
    //            break;
    //        default:
    //            Console.Clear();
    //            Console.WriteLine("You must enter a valid option");
    //            Console.ReadKey();
    //            break;
    //    }
    //}


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
        var customerForm = CustomerFactory.Create();
        var contactPersonForm = ContactPersonFactory.Create();
        var serviceForm = ServiceFactory.Create();
        var unitTypeForm = UnitTypeFactory.Create();
        var employeeForm = EmployeeFactory.Create();
        var roleForm = RoleFactory.Create();
        var statusTypeForm = StatusTypeFactory.Create();

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

        Console.Write("Status (Not Started/Ongoing/Finished): ");
        statusTypeForm.Status = Console.ReadLine()!;
        Console.Write("");




        Console.Clear();
        Console.WriteLine("***** Information about the customer *****");

        Console.Write("Customer name: ");
        customerForm.Name = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Contact person, first name: ");
        contactPersonForm.FirstName = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Contact person, last name: ");
        contactPersonForm.LastName = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Contact person, email address: ");
        contactPersonForm.Email = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Contact person, phone number: ");
        contactPersonForm.PhoneNumber = Console.ReadLine()!;
        Console.Write("");



        Console.Clear();
        Console.WriteLine("***** Information about the employee *****");

        Console.Write("First name: ");
        employeeForm.FirstName = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Last name: ");
        employeeForm.LastName = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Email: ");
        employeeForm.Email = Console.ReadLine()!;
        Console.Write("");

        Console.Write("Role in project: ");
        roleForm.Name = Console.ReadLine()!;
        Console.Write("");



        Console.Clear();
        Console.WriteLine("***** Information about the service *****");

        Console.Write("Name of service: ");
        serviceForm.Name = Console.ReadLine()!;
        Console.Write("");


        int quantity;
        while (true)
        {
            Console.Write("Quantity: ");
            string? inputQuantity = Console.ReadLine();
            if (int.TryParse(inputQuantity, out quantity))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid quantity! Please enter a numeric value.");
            }
        }
        projectForm.QuantityofServiceUnits = quantity;
        Console.Write("");


        decimal pricePerUnit;
        while (true)
        {
            Console.Write("Price/unit: ");
            string? inputPricePerUnit = Console.ReadLine();
            if (decimal.TryParse(inputPricePerUnit, out pricePerUnit))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid price! Please enter a numeric value.");
            }
        }
        serviceForm.PricePerUnit = pricePerUnit;
        Console.Write("");

        Console.Write("Service unit: ");
        unitTypeForm.Unit = Console.ReadLine()!;
        Console.Write("");


       


        Console.ReadKey();
    }

    public async Task ViewAllProjects()
    {
        Console.Clear();
        await _projectService.GetAllProjectsWithDetailsAsync(); 
        Console.ReadKey();
    }

    public async Task ViewOneProject()
    {
        Console.Clear();
        Console.WriteLine("Metod view one");
        Console.ReadKey();
    }

    public async Task UpdateProject()
    {
        Console.Clear();
        Console.WriteLine("Metod update");
        Console.ReadKey();
    }

    public async Task DeleteProject()
    {
        Console.Clear();
        Console.WriteLine("Metod delete");
        Console.ReadKey();
    }

    public async Task QuitApplication()
    {
        Console.Clear();
        Console.WriteLine("Metod quit");
        Console.ReadKey();
    }


}
