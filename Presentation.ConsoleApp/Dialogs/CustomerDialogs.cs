using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class CustomerDialogs(ICustomerService customerService) : ICustomerDialogs
{
    private readonly ICustomerService _customerService = customerService;

    public async Task RunAsync()
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
                    await CreateCustomerAsync();
                    break;
                case "2":
                    await ViewAllCustomersAsync();
                    break;
                case "3":
                    await ViewOneCustomerAsync();
                    break;
                case "4":
                    await UpdateCustomerAsync();
                    break;
                case "5":
                    await DeleteCustomerAsync();
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

    public async Task CreateCustomerAsync()
    {
        Console.Clear();
        var customerRegistrationform = CustomerFactory.Create();

        Console.WriteLine("***** Creating new customer *****");
        Console.Write("Name: ");
        customerRegistrationform.Name = Console.ReadLine()!.Trim();

        var result = await _customerService.CreateAsync(customerRegistrationform);
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

    public async Task ViewAllCustomersAsync()
    {

        Console.Clear();
        var customers = await _customerService.GetAllCustomerWithContactPersonsAsync();

        if (customers.Any())
        {
            foreach (var customer in customers)
            { 
                Console.WriteLine($"ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.Name}");
                foreach (var contactPerson in customer.ContactPersons)
                {
                    Console.WriteLine($"Contact Person: {contactPerson.FirstName} {contactPerson.LastName}, <{contactPerson.Email}> , {contactPerson.PhoneNumber}");
                }
                Console.WriteLine("");
                Console.WriteLine("--------------------------------------------");
                Console.WriteLine("");
            }
        }
        else
        {
            Console.WriteLine("No customers found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task ViewOneCustomerAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the customer you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }
        var customer = await _customerService.GetCustomerWithContactPersonsByIdAsync(id);
        if (customer != null)
        {
            Console.WriteLine($"ID: {customer.Id}");
            Console.WriteLine($"Name: {customer.Name}");
            foreach (var contactPerson in customer.ContactPersons)
            {
                Console.WriteLine($"Contact Person: {contactPerson.FirstName} {contactPerson.LastName}, <{contactPerson.Email}> , {contactPerson.PhoneNumber}");
            }
        }
        else
        {
            Console.WriteLine("Customer was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateCustomerAsync()
    {
        Console.Clear();
        var customers = await _customerService.GetAllCustomerAsync();

        foreach (var customer in customers)
        { Console.WriteLine($"Customer ID: {customer.Id}, {customer.Name}"); }

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

        var result = await _customerService.UpdateCustomerAsync(id, customerUpdateForm);
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

    public async Task DeleteCustomerAsync()
    {
        Console.Clear();
        var customers = await _customerService.GetAllCustomerAsync();

        foreach (var customer in customers)
        { Console.WriteLine($"Customer ID: {customer.Id}, {customer.Name}"); }

        Console.WriteLine("---------------------------------------");
        int id;
        Console.WriteLine("Enter the ID-number for the customer you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _customerService.DeleteCustomerAsync(id);
        if (result)
        {
            Console.WriteLine("Customer was successfully deleted");
        }
        else
        {
            Console.WriteLine("Customer was not deleted");
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
