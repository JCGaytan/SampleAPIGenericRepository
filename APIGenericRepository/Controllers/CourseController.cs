using APIGenericRepository.Controllers.Base;
using APIGenericRepository.DataAccess;
using APIGenericRepository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIGenericRepository.Controllers
{
    /// <summary>
    /// Controller for managing Course entities.
    /// Inherits from the GenericController providing CRUD operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : GenericController<Course, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CourseController"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing Course data.</param>
        public CourseController(IRepository<Course, int> repository) : base(repository)
        {
            // The base constructor of GenericController is called with the provided repository.
        }
    }
}