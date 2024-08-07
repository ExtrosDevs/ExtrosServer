using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class Chapter
    {
        [Key]
        public Guid ChapterId { get; set; }

        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public bool Type { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
