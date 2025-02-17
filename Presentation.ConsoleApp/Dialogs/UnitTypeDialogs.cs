using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class UnitTypeDialogs(IUnitTypeService unitTypeService) : IUnitTypeDialogs
{
    private readonly IUnitTypeService _unitTypeService = unitTypeService;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** UNIT TYPE MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a unit type");
            Console.WriteLine("2. View all unit types");
            Console.WriteLine("3. View one unit type");
            Console.WriteLine("4. Update a unit type");
            Console.WriteLine("5. Delete a unit type");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateUnitTypeAsync();
                    break;
                case "2":
                    await ViewAllUnitTypesAsync();
                    break;
                case "3":
                    await ViewOneUnitTypeAsync();
                    break;
                case "4":
                    await UpdateUnitTypeAsync();
                    break;
                case "5":
                    await DeleteUnitTypeAsync();
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

    public async Task CreateUnitTypeAsync()
    {
        Console.Clear();
        var unitTypeRegistrationform = UnitTypeFactory.Create();

        Console.WriteLine("***** Creating new unit type *****");
        Console.Write("Name of unit type: ");
        unitTypeRegistrationform.Unit = Console.ReadLine()!.Trim();

        var result = await _unitTypeService.CreateAsync(unitTypeRegistrationform);
        if (result)
        {
            Console.WriteLine("Unit type was successfully created");
        }
        else
        {
            Console.WriteLine("Unit type was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }
    public async Task ViewAllUnitTypesAsync()
    {

        Console.Clear();
        var unitTypes = await _unitTypeService.GetAllUnitTypesAsync();

        if (unitTypes.Any())
        {
            foreach (var statusType in unitTypes)
            { Console.WriteLine($"ID: {statusType.Id}, Unit type: {statusType.Unit}"); }
        }
        else
        {
            Console.WriteLine("No unit types found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneUnitTypeAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the unit type you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var unitType = await _unitTypeService.GetUnitTypeByIdAsync(id);
        if (unitType != null)
        {
            Console.WriteLine($"ID: {unitType.Id}, Status type: {unitType.Unit}");
        }
        else
        {
            Console.WriteLine("Unit type was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateUnitTypeAsync()
    {
        Console.Clear();
        var unitTypes = await _unitTypeService.GetAllUnitTypesAsync();

        foreach (var unitType in unitTypes)
        { Console.WriteLine($"ID: {unitType.Id}, Unit type: {unitType.Unit}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the status type you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var unitTypeUpdateForm = new UnitTypeUpdateForm();
        Console.Write($"Change unit type to: ");
        unitTypeUpdateForm.Unit = Console.ReadLine()!.Trim();

        var result = await _unitTypeService.UpdateUnitTypeAsync(id, unitTypeUpdateForm);
        if (result)
        {
            Console.WriteLine("Unit type was successfully updated");
        }
        else
        {
            Console.WriteLine("Unit type was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteUnitTypeAsync()
    {
        Console.Clear();
        var unitTypes = await _unitTypeService.GetAllUnitTypesAsync();

        foreach (var unitType in unitTypes)
        { Console.WriteLine($"ID: {unitType.Id}, Unit type: {unitType.Unit}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the status type you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _unitTypeService.DeleteUnitTypeAsync(id);
        if (result)
        {
            Console.WriteLine("Unit type was successfully deleted");
        }
        else
        {
            Console.WriteLine("Unit type was not deleted");
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
