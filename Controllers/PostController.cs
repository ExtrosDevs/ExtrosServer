using ExtrosServer;
using ExtrosServer.Data;
using ExtrosServer.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

            var posts = await _context.Posts
                .Include(p => p.Owner)
                // .Include(p => p.Likes)
                .Include(p => p.PostTags)
                // .Include(p => p.Comments)
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
            post.PostTags = new List<PostTag>();
            post.Comments = new List<Comment>();


            // Add the post to the database
            _context.Posts.Add(post);
            await _context.SaveChangesAsync(); // Save changes to the database

            // Return a response
            return Ok(post);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] JsonPatchDocument<Post> patchDoc)
        {
            // Get instance of Post by id
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Post not found.");
            }

            patchDoc.ApplyTo(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterPosts( string tags=null,string title = null, string owner = null, string sortOrder = "date" )
        {
            var query = _context.Posts.Include(user => user.Owner).Include(tag => tag.PostTags).AsQueryable();
            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(p => p.Content.Contains(title));
            }
            if (!string.IsNullOrEmpty(owner))
            {
                query = query.Where(p => p.Owner.Username.Contains(owner));
            }
            if(!string.IsNullOrEmpty(tags)){
                // var splitedTags = tags.Split("#");
                var splitedTags = tags.Split(new[] { '#' }, StringSplitOptions.RemoveEmptyEntries)
                          .Select(t => t.Trim())
                          .ToList();
                query = query.Where(p => p.PostTags.Any(t => splitedTags.Contains(t.TagValue)));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(s => s.Owner.Username);
                    break;
                case "date":
                    query = query.OrderBy(s => s.CreatedAt);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(s => s.CreatedAt);
                    break;
            }
            var posts = await query.ToListAsync();
            return Ok(posts);
        }
    }
}
