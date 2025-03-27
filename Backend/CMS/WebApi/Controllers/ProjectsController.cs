using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRegistrationModel form)
        {
            if (!ModelState.IsValid && form.CustomerId < 1)
                return BadRequest();

            var result = await _projectService.CreateProjectAsync(form);
            return result ? Created("", null) : Problem();
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProject(int id, ProjectRegistrationModel form)
        {
            if (!ModelState.IsValid && form.CustomerId < 1)
                return BadRequest();

            var result = await _projectService.EditProjectAsync(id, form);
            return result ? Ok("project got updated") : BadRequest("failed to update project");
        }
    }
}
