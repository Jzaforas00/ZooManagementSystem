using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ZooManagementSystem.Data.Repositories
{
    // Generic repository implementation for any entity type
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ZooDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        // Constructor receives the database context
        public GenericRepository(ZooDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        // Get all records
        public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
            => DbSet.ToListAsync(cancellationToken);

        // Get an entity by its ID
        public async Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
            => await DbSet.FindAsync([id], cancellationToken);

        // Find entities that match a condition
        public Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => DbSet.Where(predicate).ToListAsync(cancellationToken);

        // Get the first entity that matches a condition
        public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
            => DbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        // Get queryable data (tracked by EF)
        public IQueryable<TEntity> Query() => DbSet;

        // Get queryable data without tracking (better for read-only queries)
        public IQueryable<TEntity> QueryNoTracking() => DbSet.AsNoTracking();

        // Add a new entity
        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
            => DbSet.AddAsync(entity, cancellationToken).AsTask();

        // Add multiple entities
        public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
            => DbSet.AddRangeAsync(entities, cancellationToken);

        // Update an entity
        public void Update(TEntity entity) => DbSet.Update(entity);

        // Remove a single entity
        public void Remove(TEntity entity) => DbSet.Remove(entity);

        // Remove multiple entities
        public void RemoveRange(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

        // Save changes to the database
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => Context.SaveChangesAsync(cancellationToken);
    }
}