namespace CommentsService;

public interface IPosts
{
    Task<PostDto> GetPost(Guid Id);
}
