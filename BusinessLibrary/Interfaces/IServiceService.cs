using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IServiceService
{
    Task<bool> CreateAsync(ServiceRegistrationForm form);
    Task<IEnumerable<Service>> GetAllServicesWithUnitTypeAsync();
    Task<Service?> GetServiceWithUnitTypeByIdAsync(int id);
    Task<bool> UpdateServiceAsync(int id, ServiceUpdateForm form);
    Task<bool> DeleteServiceAsync(int id);
}






