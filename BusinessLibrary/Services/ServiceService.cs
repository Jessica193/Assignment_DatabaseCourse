using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class ServiceService : IServiceService
{
    public Task<bool> Create(ServiceRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteService(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Service>> GetAllServicesWithUnitTypes()
    {
        throw new NotImplementedException();
    }

    public Task<Service?> GetServiceWithUnitTypeById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateService(int id, ServiceUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
