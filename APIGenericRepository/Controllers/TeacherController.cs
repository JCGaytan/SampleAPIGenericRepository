using APIGenericRepository.Controllers.Base;
using APIGenericRepository.DataAccess;
using APIGenericRepository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIGenericRepository.Controllers
{
    /// <summary>
    /// Controller for managing Teacher entities.
    /// Inherits from the GenericController providing CRUD operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherController : GenericController<Teacher, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeacherController"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing Teacher data.</param>
        public TeacherController(IRepository<Teacher, int> repository) : base(repository)
        {
            // The base constructor of GenericController is called with the provided repository.
        }
    }
}