using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class RoleDialogs(IRoleService roleService) : IRoleDialogs
{
    private readonly IRoleService _roleService = roleService;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** ROLE MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a role");
            Console.WriteLine("2. View all roles");
            Console.WriteLine("3. View one role");
            Console.WriteLine("4. Update a role");
            Console.WriteLine("5. Delete a role");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateRoleAsync();
                    break;
                case "2":
                    await ViewAllRolesAsync();
                    break;
                case "3":
                    await ViewOneRoleAsync();
                    break;
                case "4":
                    await UpdateRoleAsync();
                    break;
                case "5":
                    await DeleteRoleAsync();
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

    public async Task CreateRoleAsync()
    {
        Console.Clear();
        var roleRegistrationform = RoleFactory.Create();

        Console.WriteLine("***** Creating new role *****");
        Console.Write("Name of role: ");
        roleRegistrationform.Name = Console.ReadLine()!.Trim();

        var result = await _roleService.CreateAsync(roleRegistrationform);
        if (result)
        {
            Console.WriteLine("Role was successfully created");
        }
        else
        {
            Console.WriteLine("Role was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewAllRolesAsync()
    {

        Console.Clear();
        var roles = await _roleService.GetAllRolesAsync();

        if (roles.Any())
        {
            foreach (var role in roles)
            { Console.WriteLine($"ID: {role.Id}, Name: {role.Name}"); }
        }
        else
        {
            Console.WriteLine("No roles found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneRoleAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the role you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role != null)
        {
            Console.WriteLine($"ID: {role.Id}, Name: {role.Name}");
        }
        else
        {
            Console.WriteLine("Role was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateRoleAsync()
    {
        Console.Clear();
        var roles = await _roleService.GetAllRolesAsync();

        foreach (var role in roles)
        { Console.WriteLine($"ID: {role.Id}, Name: {role.Name}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the role you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var roleUpdateForm = new RoleUpdateForm();
        Console.Write($"Change name of role to: ");
        roleUpdateForm.Name = Console.ReadLine()!.Trim();

        var result = await _roleService.UpdateRoleAsync(id, roleUpdateForm);
        if (result)
        {
            Console.WriteLine("Role was successfully updated");
        }
        else
        {
            Console.WriteLine("Role was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteRoleAsync()
    {
        Console.Clear();
        var roles = await _roleService.GetAllRolesAsync();

        foreach (var role in roles)
        { Console.WriteLine($"ID: {role.Id}, Name: {role.Name}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the role you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _roleService.DeleteRoleAsync(id);
        if (result)
        {
            Console.WriteLine("Role was successfully deleted");
        }
        else
        {
            Console.WriteLine("Role was not deleted");
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


























  
