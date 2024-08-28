using Microsoft.EntityFrameworkCore;

using ExtrosServer;
using ExtrosServer.Models;
namespace ExtrosServer.Data;
public class ApplicationDBContext: DbContext{

    public  ApplicationDBContext(DbContextOptions<ApplicationDBContext>  options):base(options){

    }
      protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure many-to-many relationship
        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostID, pt.TagValue });

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostID);

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagValue);
    }
    public DbSet<User> Users{get; set;}
    public DbSet<Comment> Comments{get;}
    public DbSet<Article> Articles{get;}
    public DbSet<Course> Courses{get;}
    public DbSet<Field> Fields{get;}
    public DbSet<Like> Likes{get;}
    public DbSet<Post> Posts{get; set;}
    public DbSet<Tag> Tags{get;}
    public DbSet<PostTag> PostTags{get;}

    public DbSet<UserFollow> UserFollows{get;}
 
    



}
