
using Microsoft.EntityFrameworkCore;

namespace PostsService;

public class PostService : Ipost
{
    private readonly ChatDbContext _context;

    public PostService(ChatDbContext context)
    {
        _context=context;
        
    }
    
    public async Task<string> CreatePost(Post newPost)
    {
        _context.Posts.AddAsync(newPost);
        await _context.SaveChangesAsync();
        return "Post created successfully";
    }

    public async Task<string> DeletePost(Post post)
    {
         _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return "Post deleted successfully";
    }

    public async Task<List<Post>> GetAllPosts()
    {
        var posts=await _context.Posts.ToListAsync();
        return posts;

    }

    public async Task<Post> GetOnePost(Guid Id)
    {
        var singlePost= await _context.Posts.Where(k=>k.Id==Id).FirstOrDefaultAsync();
        if(singlePost==null){
            return new Post();
        }
        return singlePost;
    }

    public async Task<string> UpdatePost()
    {
        await _context.SaveChangesAsync();
        return "Post updated successfully";
    }
}
