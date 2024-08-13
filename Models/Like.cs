using System.ComponentModel.DataAnnotations;

namespace ExtrosServer
{

    public class Like
    {
        [Key]
        public Guid PostID { get; set; } // Foreign Key to Post
        public Post Post { get; set; } // Navigation Property

        public string TagValue { get; set; }
    }
}