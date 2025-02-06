using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    public override Task<IEnumerable<ProjectEntity>> GetAllWithDetailsAsync(Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>> includeExpression)
    {
        var projectEntities = _context.Projects
            .Include(p => p.Customer)
            .ThenInclude(c => c.ContactPerson)
            .Include(p => p.Employee)
            .ThenInclude(e => e.Role)
            .Include(p => p.Service)
            .ThenInclude(s => s.Unit)
            .Include(p => p.StatusType)
            .ToListAsync();

        return null!;
    }

    public override Task<ProjectEntity> GetOneWithDetailsAsync(Func<IQueryable<ProjectEntity>, IQueryable<ProjectEntity>> includeExpression, Expression<Func<ProjectEntity, bool>> predicate)
    {
        return base.GetOneWithDetailsAsync(includeExpression, predicate);
    }
}
