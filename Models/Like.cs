using System.ComponentModel.DataAnnotations;

namespace ExtrosServer
{

    public class Like
    {
        public Guid PostID { get; set; } // Foreign Key to Post
        public Post Post { get; set; } // Navigation Property
        [Key]
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}