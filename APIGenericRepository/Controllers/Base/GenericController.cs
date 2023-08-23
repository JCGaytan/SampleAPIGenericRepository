using APIGenericRepository.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace APIGenericRepository.Controllers.Base
{
    /// <summary>
    /// A generic controller template for CRUD operations on entities.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TKey">The type of the entity's primary key.</typeparam>
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<TEntity, TKey> : ControllerBase where TEntity : class
    {
        private readonly IRepository<TEntity, TKey> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericController{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing entity data.</param>
        public GenericController(IRepository<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves all entities of the specified type.
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<TEntity>> GetAll()
        {
            var entities = _repository.GetAll();
            return Ok(entities);
        }

        /// <summary>
        /// Retrieves an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity.</param>
        [HttpGet("{id}")]
        public ActionResult<TEntity> GetById(TKey id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        [HttpPost]
        public ActionResult<TEntity> Create(TEntity entity)
        {
            var newEntity = _repository.Add(entity);
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(newEntity) }, newEntity);
        }

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="id">The primary key value of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        [HttpPut("{id}")]
        public ActionResult<TEntity> Update(TKey id, TEntity entity)
        {
            if (!EqualityComparer<TKey>.Default.Equals(id, GetEntityId(entity)))
                return BadRequest("The provided id does not match the entity's id.");

            var updatedEntity = _repository.Update(entity);
            if (updatedEntity == null)
                return NotFound();

            return Ok(updatedEntity);
        }

        /// <summary>
        /// Deletes an entity by its primary key.
        /// </summary>
        /// <param name="id">The primary key value of the entity to delete.</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(TKey id)
        {
            var result = _repository.Delete(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Adds a range of entities.
        /// </summary>
        /// <param name="entities">The list of entities to add.</param>
        [HttpPost("AddRange")]
        public ActionResult<List<TEntity>> AddRange([FromBody] List<TEntity> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return BadRequest("No values provided for entities.");
            }

            var insertedEntities = new List<TEntity>();
            foreach (var entity in entities)
            {
                var addedEntity = _repository.Add(entity);
                insertedEntities.Add(addedEntity);
            }

            return Ok(insertedEntities);
        }

        /// <summary>
        /// Gets the primary key value of an entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The primary key value.</returns>
        protected virtual TKey GetEntityId(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = (TKey)idProperty.GetValue(entity);
                return idValue;
            }
            throw new InvalidOperationException($"Entity {typeof(TEntity).Name} does not have an 'Id' property.");
        }
    }
}
