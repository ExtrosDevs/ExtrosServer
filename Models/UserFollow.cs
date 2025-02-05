using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class UserFollow
    {
        [Key]
        public  Guid Id{get;set;}
        public Guid FollowerId { get; set; }  // Foreign Key to User who follows
        public User Follower { get; set; }

        public Guid FollowingId { get; set; }  // Foreign Key to User who is followed
        public User Following { get; set; }
    }
}