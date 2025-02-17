namespace Presentation.ConsoleApp.Interfaces
{
    public interface IUnitTypeDialogs
    {
        Task CreateUnitTypeAsync();
        Task DeleteUnitTypeAsync();
        void QuitApplication();
        Task RunAsync();
        Task UpdateUnitTypeAsync();
        Task ViewAllUnitTypesAsync();
        Task ViewOneUnitTypeAsync();
    }
}