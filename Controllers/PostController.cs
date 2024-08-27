using ExtrosServer;
using ExtrosServer.Data;
using ExtrosServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace ExtrossServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public PostController(ApplicationDBContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {   
            var posts = await _context.Posts
                .Include(p => p.Owner)
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .ToListAsync();

            if (posts == null || posts.Any(p => p.Owner == null))
            {
                return BadRequest("Some posts have null owners.");
            }

            return Ok(posts);

        }
[HttpPost]
public async Task<IActionResult> CreatePost([FromBody] Post post)
{
    // Validate the input
    if (post == null)
    {
        return BadRequest("Post content cannot be null.");
    }

    // Check if the user exists
    var user = await _context.Users.FindAsync(post.OwnerID);
    if (user == null)
    {
        return BadRequest("Invalid OwnerID. The user does not exist.");
    }

    // Set the Owner reference
    post.Owner = user;

    // Set additional properties
    post.PostID = Guid.NewGuid(); // Generate a new ID
    post.CreatedAt = DateTime.UtcNow;
    post.UpdatedAt = DateTime.UtcNow;

    // Add the post to the database
    _context.Posts.Add(post);
    // await _context.SaveChangesAsync(); // Save changes to the database

    // Return a response
    return Ok(post);
}
        
    }
}
