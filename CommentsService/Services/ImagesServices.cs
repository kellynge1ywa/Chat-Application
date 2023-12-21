
using Microsoft.EntityFrameworkCore;

namespace CommentsService;

public class ImagesServices : IcommentImage
{
    private readonly ChatDbContext _chatDbContext;
    public ImagesServices(ChatDbContext chatDbContext)
    {
        _chatDbContext=chatDbContext;
    }
    public async Task<string> AddImage(Guid Id, CommentImages images)
    {
        var comment= _chatDbContext.Comments.Where(k=>k.CommentId==Id).FirstOrDefault();
        if(comment==null){
            return "Comment not found";
        }
        comment.Images.Add(images);
        await _chatDbContext.SaveChangesAsync();
        return "Image added";
    }
}
