namespace ExtrosServer
{

    public class Like
    {
        public Guid PostID { get; set; } // Foreign Key to Post
        public Post Post { get; set; } // Navigation Property

        public string TagValue { get; set; }
    }
}