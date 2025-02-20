using BusinessLibrary.Dtos;
using BusinessLibrary.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRegistrationForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (await _projectService.CheckIfProjectExists(x => x.Name == form.Name && x.StartDate == form.StartDate && x.EndDate == form.EndDate))
            {
                return Conflict("Project with same name, start date and end date already exists. Have you already created this project?");
            }

            var result = await _projectService.CreateAsync(form);
            if (result)
                return Ok(result);

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithDetails()
        {
            var projects = await _projectService.GetAllProjectsWithDetailsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneWithDetails(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid project ID");

            var project = await _projectService.GetProjectWithDetailsByIdAsync(id);

            if (project == null)
                return NotFound(new { message = $"Project with ID:{id} not found" });

            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProjectUpdateForm form)
        {
            if (id <= 0)
                return BadRequest("Invalid project ID");

            var result = await _projectService.UpdateProjectAsync(id, form);

            if (!result)
                return NotFound($"Project with ID{id} not found or update failed");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Invalid project ID");

            var result = await _projectService.DeleteProjectAsync(id);

            if (!result)
                return NotFound($"Project with ID{id} not found or update failed");

            return NoContent();
        }

    }
}
