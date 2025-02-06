using BusinessLibrary.Dtos;
using BusinessLibrary.Models;
using Data.Entities;

namespace BusinessLibrary.Factories;

public static class StatusTypeFactory
{
    public static StatusTypeRegistrationForm Create()
    {
        return new StatusTypeRegistrationForm();
    }

    public static StatusTypeEntity Create(StatusTypeRegistrationForm form)
    {
        return new StatusTypeEntity()
        {
            Status = form.Status,
        };
    }

    public static StatusType Create(StatusTypeEntity entity)
    {
        return new StatusType()
        {
            Id = entity.Id,
            Status = entity.Status,
        };
    }
}
