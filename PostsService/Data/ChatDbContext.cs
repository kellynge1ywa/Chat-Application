using Microsoft.EntityFrameworkCore;

namespace PostsService;

public class ChatDbContext:DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options):base(options){}

    public DbSet<Post> Posts {get;set;}
   

}
