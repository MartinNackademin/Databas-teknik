using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
    {
        public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            var entities = await _db
                .Include(x => x.Customer)
                .ToListAsync();

            return entities;
        }

        public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
        {
            var entity = await _db
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(expression);

            return entity;
        }
    }
}
