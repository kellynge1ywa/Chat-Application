namespace CommentsService;

public interface IcommentImage
{
    Task<string> AddImage(Guid Id,CommentImages images);
}
