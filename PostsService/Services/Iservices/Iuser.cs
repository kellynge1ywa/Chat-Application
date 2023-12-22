namespace PostsService;

public interface Iuser
{
    Task<ChatUserDto> GetUserById(Guid Id);

}
