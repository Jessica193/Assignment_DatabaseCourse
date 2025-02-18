using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class StatusTypeDialogs(IStatusTypeService statusTypeService) : IStatusTypeDialogs
{
    private readonly IStatusTypeService _statusTypeService = statusTypeService;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** STATUS TYPE MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a status type");
            Console.WriteLine("2. View all status types");
            Console.WriteLine("3. View one status type");
            Console.WriteLine("4. Update a status type");
            Console.WriteLine("5. Delete a status type");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateStatusTypeAsync();
                    break;
                case "2":
                    await ViewAllStatusTypesAsync();
                    break;
                case "3":
                    await ViewOneStatusTypeAsync();
                    break;
                case "4":
                    await UpdateStatusTypeAsync();
                    break;
                case "5":
                    await DeleteStatusTypeAsync();
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

    public async Task CreateStatusTypeAsync()
    {
        Console.Clear();
        var statusTypeRegistrationform = StatusTypeFactory.Create();

        Console.WriteLine("***** Creating new status type *****");
        Console.Write("Name of status type: ");
        statusTypeRegistrationform.Status = Console.ReadLine()!.Trim();

        var result = await _statusTypeService.CreateAsync(statusTypeRegistrationform);
        if (result)
        {
            Console.WriteLine("Status type was successfully created");
        }
        else
        {
            Console.WriteLine("Status type was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewAllStatusTypesAsync()
    {

        Console.Clear();
        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();

        if (statusTypes.Any())
        {
            foreach (var statusType in statusTypes)
            { Console.WriteLine($"ID: {statusType.Id}, {statusType.Status}"); }
        }
        else
        {
            Console.WriteLine("No status types found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneStatusTypeAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the status type you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var statusType = await _statusTypeService.GetStatusTypeByIdAsync(id);
        if (statusType != null)
        {
            Console.WriteLine($"ID: {statusType.Id}, {statusType.Status}");
        }
        else
        {
            Console.WriteLine("Status type was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateStatusTypeAsync()
    {
        Console.Clear();
        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();

        foreach (var statusType in statusTypes)
        { Console.WriteLine($"ID: {statusType.Id}, {statusType.Status}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the status type you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var statusTypeUpdateForm = new StatusTypeUpdateForm();
        Console.Write($"Change status type to: ");
        statusTypeUpdateForm.Status = Console.ReadLine()!.Trim();

        var result = await _statusTypeService.UpdateStatusTypeAsync(id, statusTypeUpdateForm);
        if (result)
        {
            Console.WriteLine("Status type was successfully updated");
        }
        else
        {
            Console.WriteLine("Status type was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteStatusTypeAsync()
    {
        Console.Clear();
        var statusTypes = await _statusTypeService.GetAllStatusTypesAsync();

        foreach (var statusType in statusTypes)
        { Console.WriteLine($"ID: {statusType.Id}, {statusType.Status}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the status type you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _statusTypeService.DeleteStatusTypeAsync(id);
        if (result)
        {
            Console.WriteLine("Status type was successfully deleted");
        }
        else
        {
            Console.WriteLine("Status type was not deleted");
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
