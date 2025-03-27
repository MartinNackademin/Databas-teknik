using Data.Interfaces;
using Business.Models;
using Business.Factories;
using Business.Interfaces;
using Data.Entities;

namespace Business.Services
{
   public class ProjectService(IProjectRepository projectRepository, ICustomerRepository customerRepository) : IProjectService
    {
        private readonly IProjectRepository _projectRepository = projectRepository;
        private readonly ICustomerRepository _customerRepository = customerRepository;

        public async Task<bool> CreateProjectAsync(ProjectRegistrationModel projectData)
        {
            if (!await _customerRepository.ExistsAsync(customer => customer.Id == projectData.CustomerId))
                return false;

            var projectEntity = ProjectFactory.Create(projectData);

            if (projectEntity == null)
                return false;

            bool result = await _projectRepository.AddAsync(projectEntity);
            return result;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var entities = await _projectRepository.GetAllAsync();
            var projects = entities.Select(ProjectFactory.Create);
            return projects!;
        }

        public async Task<Project> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetAsync(p => p.Id == projectId);
            if (project == null)
                return null!;
            return ProjectFactory.Create(project)!;

        }



        public async Task<bool> EditProjectAsync(int projectDatabaseID,ProjectRegistrationModel newProjectData)
        {
            var  targetProject = await _projectRepository.GetAsync(p => p.Id==projectDatabaseID);
            if (targetProject == null)
                return false;

            if (!await _customerRepository.ExistsAsync(customer => customer.Id == newProjectData.CustomerId))
                return false;

            targetProject.ProjectName = newProjectData.ProjectName;
            targetProject.ProjectDescription = newProjectData.ProjectDescription;
            targetProject.StartDate = newProjectData.StartDate;
            targetProject.EndDate = newProjectData.EndDate;
            targetProject.ProjectStatus = newProjectData.ProjectStatus;
            targetProject.CustomerId = newProjectData.CustomerId;

            bool result = await _projectRepository.UpdateAsync(targetProject);
            return result;

        }
    }
}
