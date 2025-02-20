using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using BusinessLibrary.Services;
using Presentation.ConsoleApp.Interfaces;
using System.Transactions;

namespace Presentation.ConsoleApp.Dialogs;

public class ContactPersonDialogs(IContactPersonService contactPersonService, ICustomerService customerService) : IContactPersonDialogs
{
    private readonly IContactPersonService _contactPersonService = contactPersonService;
    private readonly ICustomerService _customerService = customerService;

 

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("**** CONTACT PERSON MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a contact person");
            Console.WriteLine("2. View all contact persons");
            Console.WriteLine("3. View one contact person");
            Console.WriteLine("4. Update a contact person");
            Console.WriteLine("5. Delete a contact person");
            Console.WriteLine("6. Back to main menu");
            Console.WriteLine("7. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;

            switch (option)
            {
                case "1":
                    await CreateContactPersonAsync();
                    break;
                case "2":
                    await ViewAllContactPersonsAsync();
                    break;
                case "3":
                    await ViewOneContactPersonAsync();
                    break;
                case "4":
                    await UpdateContactPersonAsync();
                    break;
                case "5":
                    await DeleteContactPersonAsync();
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

    public async Task CreateContactPersonAsync()
    {
        Console.Clear();
        var contactPersonRegistrationform = ContactPersonFactory.Create();

        Console.WriteLine("***** LIST OF CUSTOMERS *****");
        Console.WriteLine("");
        var customers = await _customerService.GetAllCustomerAsync();

        foreach (var customer in customers)
        { Console.WriteLine($"Customer ID: {customer.Id}, {customer.Name}"); }

        int id;
        Console.WriteLine("Select a customer for the contact person (enter the ID-number): ");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        contactPersonRegistrationform.CustomerId = id;

        Console.WriteLine("***** Information absout the contact person *****");

        Console.Write("First name: ");
        contactPersonRegistrationform.FirstName = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        Console.Write("Last name: ");
        contactPersonRegistrationform.LastName = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        Console.Write("Email: ");
        contactPersonRegistrationform.Email = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        Console.Write("Phone number: ");
        contactPersonRegistrationform.PhoneNumber = Console.ReadLine()!.Trim();
        Console.WriteLine("");

        var result = await _contactPersonService.CreateAsync(contactPersonRegistrationform);
        if (result)
        {
            Console.WriteLine("Contact person was successfully created");
        }
        else
        {
            Console.WriteLine("Contact person was not created");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }


    public async Task ViewAllContactPersonsAsync()
    {

        Console.Clear();
        var contactPersons = await _contactPersonService.GetAllContactPersonsWithCustomersAsync();

        if (contactPersons.Any())
        {
            foreach (var contactPerson in contactPersons)
            {
                Console.WriteLine($"ID: {contactPerson.Id}");
                Console.WriteLine($"{contactPerson.FirstName} {contactPerson.LastName}, <{contactPerson.Email}>, {contactPerson.PhoneNumber}");
                Console.WriteLine($"Customer: {contactPerson.Customer.Name}");
                Console.WriteLine("");

            }
        }
        else
        {
            Console.WriteLine("No contact persons found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();


    }

    public async Task ViewOneContactPersonAsync()
    {
        Console.Clear();
        int id;
        Console.WriteLine("Enter the ID-number for the contact person you would like to view.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        Console.Clear() ;
        var contactPerson = await _contactPersonService.GetContactPersonByIdAsync(id);
        if (contactPerson != null)
        {
            Console.WriteLine($"ID: {contactPerson.Id}");
            Console.WriteLine($"{contactPerson.FirstName} {contactPerson.LastName}, <{contactPerson.Email}>, {contactPerson.PhoneNumber}");
            Console.WriteLine($"Customer: {contactPerson.Customer.Name}");
            Console.WriteLine("");

        }
        else
        {
            Console.WriteLine("Contact person was not found");
        }

        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task UpdateContactPersonAsync()
    {
        Console.Clear();

        Console.WriteLine("***** LIST OF CONTACT PERSONS *****");
        Console.WriteLine("");
        var contactPersons = await _contactPersonService.GetAllContactPersonsAsync();
        foreach (var contactPerson in contactPersons)
        {
            Console.WriteLine($"ID: {contactPerson.Id}, {contactPerson.FirstName} {contactPerson.LastName}, {contactPerson.Email}, {contactPerson.PhoneNumber}, {contactPerson.Customer.Name}");
        }
        Console.WriteLine("---------------------------------------");

        int id;
        Console.WriteLine("Enter the ID-number for the contact person you would like to update.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var contactPersonUpdateForm = new ContactPersonUpdateForm();
        Console.Write($"Change first name to: ");
        contactPersonUpdateForm.FirstName = Console.ReadLine()!.Trim();

        Console.Write($"Change last name to: ");
        contactPersonUpdateForm.LastName = Console.ReadLine()!.Trim();

        Console.Write($"Change email to: ");
        contactPersonUpdateForm.Email = Console.ReadLine()!.Trim();

        Console.Write($"Change phone number to: ");
        contactPersonUpdateForm.PhoneNumber = Console.ReadLine()!.Trim();

        var result = await _contactPersonService.UpdateContactPersonAsync(id, contactPersonUpdateForm);
        if (result)
        {
            Console.WriteLine("Contact person was successfully updated");
        }
        else
        {
            Console.WriteLine("Contact person was not updated");
        }
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public async Task DeleteContactPersonAsync()
    {
        Console.Clear();
        Console.WriteLine("***** LIST OF CONTACT PERSONS *****");
        Console.WriteLine("");
        var contactPersons = await _contactPersonService.GetAllContactPersonsAsync();
        foreach (var contactPerson in contactPersons)
        {
            Console.WriteLine($"ID: {contactPerson.Id}, {contactPerson.FirstName} {contactPerson.LastName}, {contactPerson.Email}, {contactPerson.PhoneNumber}, {contactPerson.Customer.Name}");
        }
        Console.WriteLine("---------------------------------------");

        int id;
        Console.WriteLine("Enter the ID-number for the contact person you would like to delete.");
        while (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.Write("Invalid input! Please enter a valid ID: ");
        }

        var result = await _contactPersonService.DeleteContactPersonAsync(id);
        if (result)
        {
            Console.WriteLine("Contact person was successfully deleted");
        }
        else
        {
            Console.WriteLine("Contact person was not deleted");
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
