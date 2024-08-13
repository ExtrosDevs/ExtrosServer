using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class Article
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public int Views { get; set; }
        [Required]
        public Guid AdminId { get; set; }
        public User Admin { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime CreateAt { get; set; }
    }

}