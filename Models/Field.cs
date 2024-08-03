using System.ComponentModel.DataAnnotations;

namespace ExtrosServer
{
    public class Field
    {
        [Required]
        public int Id { get; set; }
        [Range(0, 50)]
        public int Exp { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}