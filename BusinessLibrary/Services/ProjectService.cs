using BusinessLibrary.Dtos;
using BusinessLibrary.Factories;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BusinessLibrary.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    public async Task<bool> Create(ProjectRegistrationForm form)
    {
        if (string.IsNullOrWhiteSpace(form.Name) || form.QuantityofServiceUnits == 0 ) return false;

        var result = await _projectRepository.ExistsAsync(x => x.Name.ToLower() == form.Name.ToLower());
        if (result)
        {
            return false;
        }
        try
        {
            await _projectRepository.CreateAsync(ProjectFactory.Create(form));
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error creating project entity :: {ex.Message}");
            return false;
        }
    }
    
    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        var entities = await _projectRepository.GetAllAsync();
        var projects = entities.Select(ProjectFactory.Create).ToList();
        return projects;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsWithDetails()
    {
        var entities = await _projectRepository.GetAllWithDetailsAsync(query => query
        .Include(p => p.Customer)
        .ThenInclude(c => c.ContactPerson)
        .Include(p => p.Service)
        .ThenInclude(s => s.Unit)
        .Include(p => p.Employee)
        .ThenInclude(e => e.Role)
        .Include(p => p.StatusType));
        var projects = entities.Select(ProjectFactory.Create).ToList();
        return projects;

        //Fortsätt på detta för att bara skicka med det som behöva. Behöver jag göra en ny ProjectDto istället för Projectmodellen jag har
        //eller kan jag modifiera den?
        //return projects.Select(p => new Project {
        //    Id = p.Id,
        //    Name = p.Name,
        //    Description = p.Description,
        //    StartDate = p.StartDate,
        //    EndDate = p.EndDate,
        //    QuantityofServiceUnits = p.QuantityofServiceUnits,
        //    TotalPrice = p.TotalPrice,
        //    StatusType = p.StatusType.Status,
        //});
    }

    public async Task<Project?> GetProjectById(int id)
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

    public async Task<Project?> GetProjectWithDetailsById(int id)
    {
        var result = await _projectRepository.ExistsAsync(x => x.Id == id);

        if (result)
        {
            var entity = await _projectRepository.GetOneWithDetailsAsync(query => query
            .Include(p => p.Customer)
            .ThenInclude(c => c.ContactPerson)
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

    public async Task<bool> UpdateProject(int id, ProjectUpdateForm form)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;

        var updatedEntity = ProjectFactory.CreateUpdatedEntity(form, entity);

        try
        {
            await _projectRepository.UpdateAsync(updatedEntity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error updating project entity :: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteProject(int id)
    {
        var entity = await _projectRepository.GetOneAsync(x => x.Id == id);
        if (entity == null) return false;
        try
        {
            await _projectRepository.DeleteAsync(entity);
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error deleting project entity :: {ex.Message}");
            return false;
        }
    }

}
