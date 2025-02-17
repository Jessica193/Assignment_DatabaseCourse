namespace Presentation.ConsoleApp.Interfaces
{
    public interface IStatusTypeDialogs
    {
        Task CreateStatusTypeAsync();
        Task DeleteStatusTypeAsync();
        void QuitApplication();
        Task RunAsync();
        Task UpdateStatusTypeAsync();
        Task ViewAllStatusTypesAsync();
        Task ViewOneStatusTypeAsync();
    }
}