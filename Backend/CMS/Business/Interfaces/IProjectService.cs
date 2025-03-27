using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectRegistrationModel form);
        Task<bool> EditProjectAsync(int projectDatabaseID, ProjectRegistrationModel newProjectData);
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectByIdAsync(int id);
    }
}