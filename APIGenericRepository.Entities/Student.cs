namespace APIGenericRepository.Entities
{
    /// <summary>
    /// Represents a student entity.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the unique identifier of the student.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the course that the student is enrolled in.
        /// </summary>
        public int CourseId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the student.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the student.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email address of the student.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the student.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the student is active.
        /// </summary>
        public bool IsActive { get; set; }

        // Navigation property: A Student can be enrolled in multiple Courses
        /// <summary>
        /// Gets or sets the collection of courses that the student is enrolled in.
        /// </summary>
        public ICollection<Course>? CoursesEnrolled { get; set; }
    }
}