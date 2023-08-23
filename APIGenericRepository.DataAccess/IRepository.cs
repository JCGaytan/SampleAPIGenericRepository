namespace APIGenericRepository.DataAccess
{
    /// <summary>
    /// Represents a generic repository interface for data access.
    /// </summary>
    /// <typeparam name="TEntity">The type of entities managed by the repository.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// Retrieves all entities from the repository.
        /// </summary>
        /// <returns>An IEnumerable of entities.</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key of the entity to retrieve.</param>
        /// <returns>The retrieved entity, or null if not found.</returns>
        TEntity GetById(TKey id);

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="id">The primary key of the entity to delete.</param>
        /// <returns>True if the entity was deleted successfully, otherwise false.</returns>
        bool Delete(TKey id);

        /// <summary>
        /// Adds a range of entities to the repository.
        /// </summary>
        /// <param name="entities">The collection of entities to add.</param>
        /// <returns>An IEnumerable of added entities.</returns>
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities);
    }
}
