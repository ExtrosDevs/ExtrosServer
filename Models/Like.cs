using System.ComponentModel.DataAnnotations;

namespace ExtrosServer
{

    public class Like
    {
        [Key]
        public Guid LikeID { get; set; }
        public Guid PostID { get; set; } // Foreign Key to Post
        public Post Post { get; set; } // Navigation Property
        public Guid UserID { get; set; }
        public User User { get; set; }
    }
}