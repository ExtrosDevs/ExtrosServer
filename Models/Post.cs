using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExtrosServer.Models;

namespace ExtrosServer
{
    public class Post
    {
        [Key]
        public Guid PostID { get; set; }

        [Required]
        [StringLength(1000)]  // Example constraint for content length
        public string Content { get; set; }

        [Required]
        public Guid OwnerID { get; set; }
        // Navigation property to User
        [ForeignKey("OwnerID")]
        public User Owner { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        public string Media { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public Post()
        {
            PostID = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}