using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class Course
    {
        [Key]
        public Guid CourseId { get; set; }

        [Required]
        public string CourseName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 

        public Guid UserId { get; set; }
        public User Admin { get; set; }
    }
}
