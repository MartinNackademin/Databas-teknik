using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class ProjectFactory
    {
        public static ProjectEntity? Create(ProjectRegistrationModel form) => form == null ? null : new()
        {
            ProjectName = form.ProjectName,
            ProjectDescription = form.ProjectDescription,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            ProjectStatus = form.ProjectStatus,
            CustomerId = form.CustomerId,

        };

        public static Project? Create(ProjectEntity entity)
        {
            if (entity == null)
                return null;

            var project = new Project
            {
                Id = entity.Id,
                ProjectName = entity.ProjectName,
                ProjectDescription = entity.ProjectDescription,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ProjectStatus = entity.ProjectStatus
            };

            if (entity.Customer != null)
            {
                project.Customer = new Customer
                {
                    Id = entity.Customer.Id,
                    CustomerName = entity.Customer.CustomerName,
                    Email = entity.Customer.Email
                };
            }

            return project;
        }
    }
}
