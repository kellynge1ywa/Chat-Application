namespace CommentsService;

public interface IComment
{
    Task<string> AddComent(Comment comment);
    Task<Comment> GetComment(Guid Id);

    Task<List<CommentImageResponseDto>> GetComments(Guid Id);
    Task<string> UpdateComment();
    Task<string> DeleteComment(Comment comment);



}
