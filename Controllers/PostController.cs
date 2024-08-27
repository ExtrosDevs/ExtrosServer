using ExtrosServer;
using ExtrosServer.Data;
using ExtrosServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExtrosServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public PostController(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            // Ensure the database context is properly initialized
            if (_context.Posts == null)
            {
                return StatusCode(500, "Database context not initialized.");
            }

            // Check if there are any posts in the database
            if (!await _context.Posts.AnyAsync())
            {
                return NotFound("No posts found in the database.");
            }

            // Fetch posts with related entities
            var posts = await _context.Posts
                .Include(p => p.Owner)
                .Include(p => p.Likes)
                .Include(p => p.Tags)
                .Include(p => p.Comments)
                .ToListAsync();

            // Return posts
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
            post.Likes = new List<Like>();
            post.Tags = new List<Tag>();
            post.Comments = new List<Comment>();


            // Add the post to the database
            _context.Posts.Add(post);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return a response
            return Ok(post);
        }
    }
}
