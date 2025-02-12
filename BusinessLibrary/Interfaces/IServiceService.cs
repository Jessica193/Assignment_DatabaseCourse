using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IServiceService
{
    Task<bool> Create(ServiceRegistrationForm form);
    Task<IEnumerable<Service>> GetAllServicesWithUnitType();
    Task<Service?> GetServiceWithUnitTypeById(int id);
    Task<bool> UpdateService(int id, ServiceUpdateForm form);
    Task<bool> DeleteService(int id);
}






