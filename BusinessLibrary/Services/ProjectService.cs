using BusinessLibrary.Dtos;
using BusinessLibrary.Interfaces;
using BusinessLibrary.Models;

namespace BusinessLibrary.Services;

public class ProjectService : IProjectService
{
    public Task<bool> Create(ProjectRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProject(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetAllProjects()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetAllProjectsWithDetails()
    {
        throw new NotImplementedException();
    }

    public Task<Project?> GetProjectById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Project?> GetProjectWithDetailsById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProject(int id, ProjectUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
