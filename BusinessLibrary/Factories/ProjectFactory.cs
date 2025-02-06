using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create()
    {
        return new ProjectRegistrationForm();
    }

    public static ProjectEntity Create(ProjectRegistrationForm form)
    {
        return new ProjectEntity()
        {
            Name = form.Name,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            TotalPrice = form.TotalPrice,
        };
    }

    public static Project Create(ProjectEntity entity)
    {
        var service = entity.Service;

        return new Project()
        {
            Id = entity.Id,
            Name= entity.Name,
            Description= entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            TotalPrice = entity.TotalPrice,
           
            

        };
    }
    
}
