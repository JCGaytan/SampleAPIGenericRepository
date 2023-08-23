namespace APIGenericRepository.Entities
{
    /// <summary>
    /// Represents a course entity.
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Gets or sets the unique identifier of the course.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the course.
        /// </summary>
        public string Title { get; set; }

        // Navigation properties

        /// <summary>
        /// Gets or sets the identifier of the teacher associated with the course.
        /// </summary>
        public int TeacherId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the course.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the course is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the teacher associated with the course.
        /// </summary>
        public Teacher? Teacher { get; set; }

        /// <summary>
        /// Gets or sets the collection of students enrolled in the course.
        /// </summary>
        public ICollection<Student>? StudentsEnrolled { get; set; }
    }
}