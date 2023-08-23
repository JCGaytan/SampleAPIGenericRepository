namespace APIGenericRepository.Entities
{
    /// <summary>
    /// Represents a teacher entity.
    /// </summary>
    public class Teacher
    {
        /// <summary>
        /// Gets or sets the unique identifier of the teacher.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the teacher.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the teacher.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the teacher.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the teacher.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the teacher is active.
        /// </summary>
        public bool IsActive { get; set; }

        // Navigation property: A Teacher can teach multiple Courses
        /// <summary>
        /// Gets or sets the collection of courses that the teacher is teaching.
        /// </summary>
        public ICollection<Course>? CoursesTaught { get; set; }
    }
}