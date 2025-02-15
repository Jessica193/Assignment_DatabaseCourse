using BusinessLibrary.Dtos;
using BusinessLibrary.Models;

namespace BusinessLibrary.Interfaces;

public interface IProjectService
{
    Task<bool> CreateAsync(ProjectRegistrationForm form);
    Task<IEnumerable<Project>> GetAllProjectsAsync();
    Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync();
    Task<Project?> GetProjectByIdAsync(int id);
    Task<Project?> GetProjectWithDetailsByIdAsync(int id);
    Task<bool> UpdateProjectAsync(int id, ProjectUpdateForm form);
    Task<bool> DeleteProjectAsync(int id);
}






