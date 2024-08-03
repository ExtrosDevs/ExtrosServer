namespace ExtrosServer.Models
{
    public class UserFollow
    {
        public int FollowerId { get; set; }  // Foreign Key to User who follows
        public User Follower { get; set; }

        public int FollowingId { get; set; }  // Foreign Key to User who is followed
        public User Following { get; set; }
    }
}