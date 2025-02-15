using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class CustomerDialogs(ICustomerService customerService) : ICustomerDialogs
{
    private readonly ICustomerService _customerService = customerService;

    public async Task Run()
    {
        while(true)
        {
            Console.Clear();
            Console.WriteLine("**** CUSTOMER MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a customer");
            Console.WriteLine("2. View all customers");
            Console.WriteLine("3. View one customer");
            Console.WriteLine("4. Update a customer");
            Console.WriteLine("5. Delete a customer");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateCustomer();
                    break;
                case "2":
                    await ViewAllCustomers();
                    break;
                case "3":
                    await ViewOneCustomer();
                    break;
                case "4":
                    await UpdateCustomer();
                    break;
                case "5":
                    await DeleteCustomer();
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

    public async Task CreateCustomer()
    {
        Console.Clear();
        var customerRegistrationform = CustomerFactory.Create();

        Console.WriteLine("***** Creating new customer *****");
        Console.Write("Name: ");
        customerRegistrationform.Name = Console.ReadLine()!.Trim();

        var result = await _customerService.Create(customerRegistrationform);
        if (result)
        {
            Console.WriteLine("Customer was successfully created");
        }
        else
        {
            Console.WriteLine("Customer was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewAllCustomers()
    {

        Console.Clear();
        var customers = await _customerService.GetAllCustomer();

        if (customers.Any())
        {
            foreach (var customer in customers)
            { Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}"); }
        }
        else
        {
            Console.WriteLine("No customers found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneCustomer()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the customer you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var customer = await _customerService.GetCustomerById(id);
        if (customer != null)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}");
        }
        else
        {
            Console.WriteLine("Customer was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateCustomer()
    {
        Console.Clear();
        var customers = await _customerService.GetAllCustomer();

        foreach (var customer in customers)
        { Console.WriteLine($"Customer ID: {customer.Id}, Customer name: {customer.Name}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the customer you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var customerUpdateForm = new CustomerUpdateForm();
        Console.Write($"Change name to: ");
        customerUpdateForm.Name = Console.ReadLine()!.Trim();

        var result = await _customerService.UpdateCustomer(id, customerUpdateForm);
        if (result)
        {
            Console.WriteLine("Customer was successfully updated");
        }
        else
        {
            Console.WriteLine("Customer was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteCustomer()
    {
        Console.Clear();
        var customers = await _customerService.GetAllCustomer();

        foreach (var customer in customers)
        { Console.WriteLine($"Customer ID: {customer.Id}, Customer name: {customer.Name}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the customer you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _customerService.DeleteCustomer(id);
        if (result)
        {
            Console.WriteLine("CustomerEntity was successfully deleted");
        }
        else
        {
            Console.WriteLine("CustomerEntity was not deleted");
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
