using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ExtrosServer.Models;

namespace ExtrosServer
{

    public class Tag
    {
         [Key]
        public string TagValue { get; set; }
        [JsonIgnore]
        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    }
}