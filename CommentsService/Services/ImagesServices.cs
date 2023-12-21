
using Microsoft.EntityFrameworkCore;

namespace CommentsService;

public class ImagesServices : IcommentImage
{
    private readonly ChatDbContext _chatDbContext;
    public ImagesServices(ChatDbContext chatDbContext)
    {
        _chatDbContext=chatDbContext;
    }
    public async Task<string> AddImage(Guid Id, CommentImages Images)
    {
        var comment= _chatDbContext.Comments.Where(k=>k.CommentId==Id).FirstOrDefault();
        comment.Images.Add(Images);
        await _chatDbContext.SaveChangesAsync();
        return "Images added";
    }
}
