namespace Presentation.ConsoleApp.Interfaces
{
    public interface IServiceDialogs
    {
        Task CreateServiceAsync();
        Task DeleteServiceAsync();
        void QuitApplication();
        Task RunAsync();
        Task UpdateServiceAsync();
        Task ViewAllServicesAsync();
        Task ViewOneServiceAsync();
    }
}