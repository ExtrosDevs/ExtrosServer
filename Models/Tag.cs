using System.ComponentModel.DataAnnotations;

namespace ExtrosServer
{

    public class Tag
    {
        public Guid PostID { get; set; } // Foreign Key to Post
        public Post Post { get; set; } // Navigation Property
        [Key]
        public string TagValue { get; set; }
    }
}