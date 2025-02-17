namespace Presentation.ConsoleApp.Interfaces
{
    public interface IEmployeeDialogs
    {
        Task CreateEmployeeAsync();
        Task DeleteEmployeeAsync();
        void QuitApplication();
        Task RunAsync();
        Task UpdateEmployeeAsync();
        Task ViewAllEmployeesAsync();
        Task ViewOneEmployeeAsync();
    }
}