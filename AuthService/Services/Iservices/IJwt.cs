namespace AuthService;

public interface IJwt
{
    string GenerateToken(ChatUser chatUser, IEnumerable<string> Roles);

}
