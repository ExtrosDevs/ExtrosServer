using Microsoft.EntityFrameworkCore;

using ExtrosServer;
using ExtrosServer.Models;
namespace ExtrosServer.Data;
public class ApplicationDBContext: DbContext{

    public  ApplicationDBContext(DbContextOptions<ApplicationDBContext>  options):base(options){

    }
        public DbSet<User> Users{get;}
    public DbSet<Comment> Comments{get;}
    public DbSet<Article> Articles{get;}
    public DbSet<Course> Courses{get;}
    public DbSet<Field> Fields{get;}
    public DbSet<Like> Likes{get;}
    public DbSet<Post> Posts{get;}
    public DbSet<Tag> Tags{get;}
    public DbSet<UserFollow> UserFollows{get;}
 
    



}
