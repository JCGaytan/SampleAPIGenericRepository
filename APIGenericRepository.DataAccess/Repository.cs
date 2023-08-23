using Microsoft.EntityFrameworkCore;

namespace APIGenericRepository.DataAccess
{
    /// <summary>
    /// Represents a generic repository implementation for data access using Entity Framework.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        /// <inheritdoc/>
        public TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        /// <inheritdoc/>
        public TEntity Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        /// <inheritdoc/>
        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        /// <inheritdoc/>
        public bool Delete(TKey id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        /// <inheritdoc/>
        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
            return entities;
        }
    }
}
