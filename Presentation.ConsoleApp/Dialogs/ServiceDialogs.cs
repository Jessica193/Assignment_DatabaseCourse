using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class ServiceDialogs(IServiceService serviceService, IUnitTypeService unitTypeService) : IServiceDialogs
{
    private readonly IServiceService _serviceService = serviceService;
    private readonly IUnitTypeService _unitTypeService = unitTypeService;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** SERVICE MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a service");
            Console.WriteLine("2. View all services");
            Console.WriteLine("3. View one service");
            Console.WriteLine("4. Update a service");
            Console.WriteLine("5. Delete a service");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateServiceAsync();
                    break;
                case "2":
                    await ViewAllServicesAsync();
                    break;
                case "3":
                    await ViewOneServiceAsync();
                    break;
                case "4":
                    await UpdateServiceAsync();
                    break;
                case "5":
                    await DeleteServiceAsync();
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

    public async Task CreateServiceAsync()
    {
        Console.Clear();
        var serviceRegistrationform = ServiceFactory.Create();

        Console.WriteLine("***** LIST OF UNIT TYPES *****");
        Console.WriteLine("");
        var unitTypes = await _unitTypeService.GetAllUnitTypesAsync();

        foreach (var unitType in unitTypes)
        { Console.WriteLine($"ID: {unitType.Id}, {unitType.Unit}"); }
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");

        int id;
        Console.WriteLine("Select unit for the service (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        serviceRegistrationform.UnitTypeId = id;

        Console.Write("Name of service: ");
        serviceRegistrationform.Name = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        decimal price;
        Console.Write("Price per unit: ");
        while (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Invalid input! Please enter a valid decimal number: ");
        }
        serviceRegistrationform.PricePerUnit = price;


        var result = await _serviceService.CreateAsync(serviceRegistrationform);
        if (result)
        {
            Console.WriteLine("Service was successfully created");
        }
        else
        {
            Console.WriteLine("Service was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }
    public async Task ViewAllServicesAsync()
    {

        Console.Clear();
        var services = await _serviceService.GetAllServicesWithUnitTypeAsync();

        if (services.Any())
        {
            foreach (var service in services)
            { Console.WriteLine($"ID: {service.Id}, {service.Name}, Price/unit: {service.PricePerUnit}, Unit: {service.Unit.Unit}"); }
        }
        else
        {
            Console.WriteLine("No services found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneServiceAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the service you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var service = await _serviceService.GetServiceWithUnitTypeByIdAsync(id);
        if (service != null)
        {
            Console.WriteLine($"ID: {service.Id}, {service.Name}, Price/unit: {service.PricePerUnit}, Unit: {service.Unit.Unit}");
        }
        else
        {
            Console.WriteLine("Service was not found");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }
    public async Task UpdateServiceAsync()
    {
        Console.Clear();
        var services = await _serviceService.GetAllServicesWithUnitTypeAsync();

        foreach (var service in services)
        { Console.WriteLine($"ID: {service.Id}, {service.Name}, Price/unit: {service.PricePerUnit}, Unit: {service.Unit.Unit}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the service you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var serviceUpdateForm = new ServiceUpdateForm();
        Console.Write($"Change name of service to: ");
        serviceUpdateForm.Name = Console.ReadLine()!.Trim();

        decimal price;
        Console.Write("Change price/unit to: ");
        while (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.Write("Invalid input! Please enter a valid decimal number: ");
        }
        serviceUpdateForm.PricePerUnit = price;

        var result = await _serviceService.UpdateServiceAsync(id, serviceUpdateForm);
        if (result)
        {
            Console.WriteLine("Service was successfully updated");
        }
        else
        {
            Console.WriteLine("Service was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteServiceAsync()
    {
        Console.Clear();
        var services = await _serviceService.GetAllServicesWithUnitTypeAsync();

        foreach (var service in services)
        { Console.WriteLine($"ID: {service.Id}, {service.Name}, Price/unit: {service.PricePerUnit}, Unit: {service.Unit.Unit}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the service you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _serviceService.DeleteServiceAsync(id);
        if (result)
        {
            Console.WriteLine("Service was successfully deleted");
        }
        else
        {
            Console.WriteLine("Service was not deleted");
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
