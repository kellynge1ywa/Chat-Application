using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthService;

public class ChatDbContext: IdentityDbContext<ChatUser>
{
    public ChatDbContext(DbContextOptions<ChatDbContext>options):base(options)
    {
        
    }

    public DbSet<ChatUser> ChatUser {get;set;}

}
