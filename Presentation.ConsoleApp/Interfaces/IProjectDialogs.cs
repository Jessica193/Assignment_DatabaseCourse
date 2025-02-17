namespace Presentation.ConsoleApp.Interfaces
{
    public interface IProjectDialogs
    {
        Task CreateProjectAsync();
        Task DeleteProjectAsync();
        void QuitApplication();
        Task RunAsync();
        Task UpdateProjectAsync();
        Task ViewAllProjectsAsync();
        Task ViewOneProjectAsync();
    }
}