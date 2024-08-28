using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExtrosServer.Models{
public class PostTag
{
    [Key]
    public Guid PostTagID{get;set;}
    public Guid PostID { get; set; }
    public Post Post { get; set; }

    [ForeignKey(name:"Tag")]
    public String TagValue { get; set; }
    public Tag Tag { get; set; }
}
}
