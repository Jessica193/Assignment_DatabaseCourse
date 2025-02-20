using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Entities;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace BusinessLibrary.Services;

public class ProjectService(IProjectRepository projectRepository, IServiceRepository serviceRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;
    private readonly IServiceRepository _serviceRepository = serviceRepository;

    public async Task<bool> CreateAsync(ProjectRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name) || form.QuantityofServiceUnits == 0) return false;

        var result = await _projectRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }

        await _projectRepository.BeginTransactionAsync();

        try
        {
            var projectEntity = await ProjectFactory.CreateAsync(form, _serviceRepository);
            await _projectRepository.CreateAsync(projectEntity);
            await _projectRepository.SaveToDatabaseAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error creating project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create).ToList();
        return projects;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync()
    {
        var entities = await _projectRepository.GetAllWithDetailsAsync(query => query
        .Include(p => p.Customer)
        .ThenInclude(c => c.ContactPersons)
        .Include(p => p.Service)
        .ThenInclude(s => s.Unit)
        .Include(p => p.Employee)
        .ThenInclude(e => e.Role)
        .Include(p => p.StatusType));
        var projects = entities.Select(ProjectFactory.Create).ToList();
        return projects;
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var result = await _projectRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
            var project = ProjectFactory.Create(entity);
            return project;
        }
        return null;
    }

    public async Task<Project?> GetProjectWithDetailsByIdAsync(int id)
    {
        var result = await _projectRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _projectRepository.GetOneWithDetailsAsync(query => query
            .Include(p => p.Customer)
            .ThenInclude(c => c.ContactPersons)
            .Include(p => p.Service)
            .ThenInclude(s => s.Unit)
            .Include(p => p.Employee)
            .ThenInclude(e => e.Role)
            .Include(p => p.StatusType),
            x => x.Id == id);
            var project = ProjectFactory.Create(entity);
            return project;
        }
        return null;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        ProjectFactory.CreateUpdatedEntity(form, entity);

        await _projectRepository.BeginTransactionAsync();

        try
        {
            _projectRepository.Update(entity);
            await _projectRepository.SaveToDatabaseAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error updating project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        await _projectRepository.BeginTransactionAsync();

        try
        {
            _projectRepository.Delete(entity);
            await _projectRepository.SaveToDatabaseAsync();
            await _projectRepository.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex)
        {
            await _projectRepository.RollbackTransactionAsync();
            Debug.WriteLine($"Error deleting project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> CheckIfProjectExists(Expression<Func<ProjectEntity, bool>> predicate)
    {
        return await _projectRepository.ExistsAsync(predicate);
    }

}
