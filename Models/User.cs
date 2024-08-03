using System.ComponentModel.DataAnnotations;
using ExtrosServer.Models;

namespace ExtrosServer
{
    public class User
    {
        [Required]
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]

        public required string Password { get; set; }
        [Required]
        [EmailAddress]
        public required string Eamil { get; set; }

        public DateTime? LastLoginDate { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateOnly BirthDate { get; set; }
        public bool Verified { get; set; }
        public int PhoneNumber { get; set; }
        public string UserImage { get; set; }
        public string Bio { get; set; }
        public int PostalCode { get; set; }
        public bool IsAdmin { get; set; }
        // RoleId is forignKey for field table
        // and the UserField is a Object instacne from class User
        public int RoleId { get; set; }
        public required Field UserField { get; set; }

        // Navigation properties for the following relationship
        public ICollection<UserFollow> Followers { get; set; }  // Users who follow this user
        public ICollection<UserFollow> Following { get; set; }  // Users this user is following
    }
}