using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class EmployeeDialogs(IEmployeeService employeeService, IRoleService roleService) : IEmployeeDialogs
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IRoleService _roleService = roleService;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** EMPLOYEE MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a employee");
            Console.WriteLine("2. View all employees");
            Console.WriteLine("3. View one employee");
            Console.WriteLine("4. Update an employee");
            Console.WriteLine("5. Delete an employee");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateEmployeeAsync();
                    break;
                case "2":
                    await ViewAllEmployeesAsync();
                    break;
                case "3":
                    await ViewOneEmployeeAsync();
                    break;
                case "4":
                    await UpdateEmployeeAsync();
                    break;
                case "5":
                    await DeleteEmployeeAsync();
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

    public async Task CreateEmployeeAsync()
    {
        Console.Clear();
        var employeeRegistrationform = EmployeeFactory.Create();

        Console.WriteLine("***** LIST OF ROLES *****");
        Console.WriteLine("");
        var roles = await _roleService.GetAllRolesAsync();

        foreach (var role in roles)
        { Console.WriteLine($"ID: {role.Id}, {role.Name}"); }
        Console.WriteLine("");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("");

        int id;
        Console.WriteLine("Select a role for the employee (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        employeeRegistrationform.RoleId = id;

        Console.WriteLine("***** Information about the employee *****");

        Console.Write("First name: ");
        employeeRegistrationform.FirstName = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        Console.Write("Last name: ");
        employeeRegistrationform.LastName = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        Console.Write("Email: ");
        employeeRegistrationform.Email = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        var result = await _employeeService.CreateAsync(employeeRegistrationform);
        if (result)
        {
            Console.WriteLine("Employee was successfully created");
        }
        else
        {
            Console.WriteLine("Employee was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewAllEmployeesAsync()
    {
        Console.Clear();
        var employees = await _employeeService.GetAllEmployeesWithRoleAsync();

        if (employees.Any())
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.Id}");
                Console.WriteLine($"{employee.FirstName} {employee.LastName}, <{employee.Email}>, {employee.Role.Name}");
                Console.WriteLine("");
                Console.WriteLine("--------------------------------------------------------");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No employees found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneEmployeeAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the employee you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var employee = await _employeeService.GetEmployeeWithRoleByIdAsync(id);
        if (employee != null)
        {
            Console.WriteLine($"ID: {employee.Id}");
            Console.WriteLine($"{employee.FirstName} {employee.LastName}, <{employee.Email}>, {employee.Role.Name}");
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("");
        }
        else
        {
            Console.WriteLine("Employee was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateEmployeeAsync()
    {
        Console.Clear();

        Console.WriteLine("***** LIST OF EMPLOYEES *****");
        var employees = await _employeeService.GetAllEmployeesWithRoleAsync();

        if (employees.Any())
        {
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.Id}, Name: {employee.FirstName} {employee.LastName}, Email: {employee.Email}, Role: {employee.Role.Name}");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No employees found");
        }
        Console.WriteLine("---------------------------------------");

        int id;
        Console.WriteLine("Enter the ID-number for the employee you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var employeeUpdateForm = new EmployeeUpdateForm();
        Console.Write($"Change first name to: ");
        employeeUpdateForm.FirstName = Console.ReadLine()!.Trim();

        Console.Write($"Change last name to: ");
        employeeUpdateForm.LastName = Console.ReadLine()!.Trim();

        Console.Write($"Change email to: ");
        employeeUpdateForm.Email = Console.ReadLine()!.Trim();

        var result = await _employeeService.UpdateEmployeeAsync(id, employeeUpdateForm);
        if (result)
        {
            Console.WriteLine("Employee was successfully updated");
        }
        else
        {
            Console.WriteLine("Employee was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteEmployeeAsync()
    {
        Console.Clear();
        Console.WriteLine("***** LIST OF EMPLOYEES *****");
        var employees = await _employeeService.GetAllEmployeesWithRoleAsync();
        foreach (var employee in employees)
        {
            Console.WriteLine($"ID: {employee.Id}, Name: {employee.FirstName} {employee.LastName}, Email: {employee.Email}, Role: {employee.Role.Name}");
            Console.WriteLine("");
        }
        Console.WriteLine("---------------------------------------");

        int id;
        Console.WriteLine("Enter the ID-number for the employee you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _employeeService.DeleteEmployeeAsync(id);
        if (result)
        {
            Console.WriteLine("Employee was successfully deleted");
        }
        else
        {
            Console.WriteLine("Employee was not deleted");
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
