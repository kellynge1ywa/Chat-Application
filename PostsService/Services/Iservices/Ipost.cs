namespace PostsService;

public interface Ipost
{
    Task<string> CreatePost(Post newPost);
    Task<List<Post>> GetAllPosts();

    Task<List<UserPostDto>> GetPostByUser(Guid UserId);
    Task<Post>GetOnePost(Guid Id);
    Task<string> UpdatePost();
    Task<string> DeletePost(Post post);

}
