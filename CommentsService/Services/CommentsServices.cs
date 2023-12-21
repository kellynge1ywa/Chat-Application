
using Microsoft.EntityFrameworkCore;

namespace CommentsService;

public class CommentsServices : IComment
{
    private readonly ChatDbContext _chatDbContext;
    public CommentsServices(ChatDbContext chatDbContext)
    {
        _chatDbContext=chatDbContext;
    }
    public async Task<string> AddComent(Comment comment)
    {
        _chatDbContext.Comments.Add(comment);
        await _chatDbContext.SaveChangesAsync();
        return "Comment added!!";

    }

    public async Task<string> DeleteComment(Comment comment)
    {
         _chatDbContext.Comments.Remove(comment);
         await _chatDbContext.SaveChangesAsync();
         return "Comment deleted successfully";

    }

    public async Task<Comment> GetComment(Guid Id)
    {
        try{
        return await _chatDbContext.Comments.Where(C=>C.CommentId==Id).FirstOrDefaultAsync();
        } catch(Exception ex){
            Console.WriteLine(ex.Message);
            return new Comment();
        }
    }

    public async Task<List<CommentImageResponseDto>> GetComments(Guid Id)
    {
        try{
        return await _chatDbContext.Comments.Select(C=>new CommentImageResponseDto(){
            Id=C.CommentId,
            Content=C.Content,
            PostId=C.PostId,
            Replies=C.Replies,
            Likes=C.Likes,
            Images=C.Images!.Select(I=> new AddImageDto(){
                Image=I.Image
            }).ToList()
        }).Where(p=>p.PostId==Id).ToListAsync();
        }
        catch(Exception ex){
            return new List<CommentImageResponseDto>();
        }
    }

    public async  Task<string> UpdateComment()
    {
        await _chatDbContext.SaveChangesAsync();
        return "Comment updated successfully!!";
    }
}
