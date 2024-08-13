using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class Category
    {
        [Key]
        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
