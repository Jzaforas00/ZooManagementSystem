using System.Linq.Expressions;

namespace ZooManagementSystem.Data.Repositories
{
    // Generic repository for any entity type
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        // Get all records
        Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

        // Get one record by its ID
        Task<TEntity?> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        // Find records using a condition
        Task<List<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        // Get the first record that matches a condition
        Task<TEntity?> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default);

        // Get queryable data (with tracking)
        IQueryable<TEntity> Query();

        // Get queryable data without tracking (better performance for read-only)
        IQueryable<TEntity> QueryNoTracking();

        // Add a new entity
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        // Add multiple entities
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        // Update an entity
        void Update(TEntity entity);

        // Remove an entity
        void Remove(TEntity entity);

        // Remove multiple entities
        void RemoveRange(IEnumerable<TEntity> entities);

        // Save changes to the database
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}