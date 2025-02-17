namespace Presentation.ConsoleApp.Interfaces
{
    public interface IRoleDialogs
    {
        Task CreateRoleAsync();
        Task DeleteRoleAsync();
        void QuitApplication();
        Task RunAsync();
        Task UpdateRoleAsync();
        Task ViewAllRolesAsync();
        Task ViewOneRoleAsync();
    }
}