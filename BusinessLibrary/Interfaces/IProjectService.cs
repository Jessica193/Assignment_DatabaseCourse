using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IProjectService
{
    Task<bool> Create(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetAllProjects();
    Task<IEnumerable<Project>> GetAllProjectsWithDetails();
    Task<Project?> GetProjectById(int id);
    Task<Project?> GetProjectWithDetailsById(int id);
    Task<bool> UpdateProject(int id, ProjectUpdateForm form);
    Task<bool> DeleteProject(int id);
}






