using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class Enrollment
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Key]
        public DateTime EnrollmentId { get; set; }

        [Required]
        public float Progress { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}
