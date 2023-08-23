using APIGenericRepository.Controllers.Base;
using APIGenericRepository.DataAccess;
using APIGenericRepository.Entities;
using Microsoft.AspNetCore.Mvc;

namespace APIGenericRepository.Controllers
{
    /// <summary>
    /// Controller for managing Student entities.
    /// Inherits from the GenericController providing CRUD operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : GenericController<Student, int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentController"/> class.
        /// </summary>
        /// <param name="repository">The repository for accessing Student data.</param>
        public StudentController(IRepository<Student, int> repository) : base(repository)
        {
            // The base constructor of GenericController is called with the provided repository.
        }
    }
}