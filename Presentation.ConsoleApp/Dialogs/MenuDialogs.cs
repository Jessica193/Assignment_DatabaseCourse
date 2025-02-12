using Presentation.ConsoleApp.Interfaces;

namespace Presentation.ConsoleApp.Dialogs;

public class MenuDialogs : IMenuDialogs
{
    public void Run()
    {
        while (true)
        {
            Console.Clear();
            var option = MainMenu();
            if (!string.IsNullOrEmpty(option))
            {
                OptionSwitch(option);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You must enter an option");
            }
        }
    }

    public string MainMenu()
    {
        
            Console.Clear();
            Console.WriteLine("**** MAIN MENU *****");
            Console.WriteLine("");
            Console.WriteLine("1. Create a project");
            Console.WriteLine("2. View all projects");
            Console.WriteLine("3. View one project");
            Console.WriteLine("4. Update project");
            Console.WriteLine("5. Delete project");
            Console.WriteLine("6. Quit application");
            Console.WriteLine("----------------------------------------");
            Console.Write("Enter your option: ");

            var option = Console.ReadLine()!;
            return option;
      
    }

    public void OptionSwitch(string option)
    {
        switch (option)
        {
            case "1":
                CreateProject();
                break;
            case "2":
                ViewAllProjects();
                break;
            case "3":
                ViewOneProject();
                break;
            case "4":
                UpdateProject();
                break;
            case "5":
                DeleteProject();
                break;
            case "6":
                QuitApplication();
                break;
            default:
                Console.Clear();
                Console.WriteLine("You must enter a valid option");
                Console.ReadKey();
                break;
        }
    }

    public void CreateProject()
    {
        Console.Clear();
        Console.WriteLine("Metod create");
        Console.ReadKey();
    }

    public void ViewAllProjects()
    {
        Console.Clear();
        Console.WriteLine("Metod view all");
        Console.ReadKey();
    }

    public void ViewOneProject()
    {
        Console.Clear();
        Console.WriteLine("Metod view one");
        Console.ReadKey();
    }

    public void UpdateProject()
    {
        Console.Clear();
        Console.WriteLine("Metod update");
        Console.ReadKey();
    }

    public void DeleteProject()
    {
        Console.Clear();
        Console.WriteLine("Metod delete");
        Console.ReadKey();
    }

    public void QuitApplication()
    {
        Console.Clear();
        Console.WriteLine("Metod quit");
        Console.ReadKey();
    }


}
