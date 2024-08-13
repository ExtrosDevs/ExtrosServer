using System.ComponentModel.DataAnnotations;

namespace ExtrosServer.Models
{
    public class CategorySelection
    {
        [Required]
        public Guid CourseId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        //navigation properties
        public Course Course { get; set; }
        public Category Category { get; set; }
    }
}
