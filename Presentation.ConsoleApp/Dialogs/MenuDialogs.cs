using BusinessLibrary.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs(ICustomerDialogs customerDialogs, IContactPersonDialogs contactPersonDialogs, IEmployeeDialogs employeeDialogs, IRoleDialogs roleDialogs, IServiceDialogs serviceDialogs, IStatusTypeDialogs statusTypeDialogs, IUnitTypeDialogs unitTypeDialogs, IProjectDialogs projectDialogs) : IMenuDialogs
{
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

    public void QuitApplication()
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
