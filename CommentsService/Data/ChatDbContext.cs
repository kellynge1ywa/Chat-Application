using Microsoft.EntityFrameworkCore;

namespace CommentsService;

public class ChatDbContext:DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext>options):base(options)
    {
        
    }
    public DbSet<Comment> Comments {get;set;}
    public DbSet<CommentImages> CommentImages {get;set;}

}
//dotnet ef migrations add InitialMigration

