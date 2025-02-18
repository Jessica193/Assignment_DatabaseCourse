using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class ServiceService(IServiceRepository serviceRepository) : IServiceService
{
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<bool> CreateAsync(ServiceRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name) || form.PricePerUnit == 0) return false;

        var result = await _serviceRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _serviceRepository.CreateAsync(ServiceFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating service entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<Service>> GetAllServicesWithUnitTypeAsync()
    {
        var entities = await _serviceRepository.GetAllWithDetailsAsync(query => query.Include(s => s.Unit));
        var services = entities.Select(ServiceFactory.Create).ToList();
        return services;
    }

    public async Task<Service?> GetServiceWithUnitTypeByIdAsync(int id)
    {
        var result = await _serviceRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _serviceRepository.GetOneWithDetailsAsync(query => query.Include(s => s.Unit), x => x.Id == id);
            var service = ServiceFactory.Create(entity);
            return service;
        }
        return null;
    }

    public async Task<bool> UpdateServiceAsync(int id, ServiceUpdateForm form)
    {
        var entity = await _serviceRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        ServiceFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _serviceRepository.UpdateAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating service entity :: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteServiceAsync(int id)
    {
        var entity = await _serviceRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _serviceRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting service entity :: {ex.Message}");
            return false;
        }
    }
}
