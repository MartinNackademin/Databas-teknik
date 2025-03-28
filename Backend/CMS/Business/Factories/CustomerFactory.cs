﻿using Business.Models;
using Data.Entities;

namespace Business.Factories
{
    public static class CustomerFactory
    {
        public static CustomerEntity? Create(CustomerRegistrationModel form) => form == null ? null : new()
        {
            CustomerName = form.CustomerName,
            Email = form.Email
        };
        public static Customer? Create(CustomerEntity entity)
        {
            if (entity == null)
            return null;
            
           var customer = new Customer()
           {
                Id = entity.Id,
                CustomerName = entity.CustomerName,
                Email = entity.Email,
                Projects = []
            };

            if (entity.Projects != null)
            {
                var projects = new List<Project>();

                foreach (var project in entity.Projects)
                {
                    projects.Add(new Project
                    {
                        Id = project.Id,
                        ProjectName = project.ProjectName,
                        ProjectDescription = project.ProjectDescription,
                        StartDate = project.StartDate,
                        EndDate = project.EndDate,
                        ProjectStatus = project.ProjectStatus
                    });
                }

                customer.Projects = projects;
            }
            return customer;
    
        }
        }
    }

