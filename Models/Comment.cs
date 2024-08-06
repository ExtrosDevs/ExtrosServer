namespace ExtrosServer
{

    public class Comment
    {
        public Guid CommentID { get; set; }
        public string Content { get; set; }
        public Guid PostID { get; set; } // Foreign Key to Post
        public Post Post { get; set; } // Navigation Property

        public Guid UserID { get; set; } // Foreign Key to User
        public User User { get; set; } // Navigation Property

        public Guid? ReplyToCommentID { get; set; } // Optional Foreign Key to Comment (for replies)
        public Comment ReplyToComment { get; set; } // Navigation Property

        public ICollection<Comment> Replies { get; set; } = new List<Comment>();

        public Comment()
        {
            CommentID = Guid.NewGuid();
        }
    }
}