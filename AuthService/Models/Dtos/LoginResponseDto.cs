namespace AuthService;

public class LoginResponseDto
{
    public string Token {get;set;}=string.Empty;
    public ChatUserDto User {get;set;}= default!;

}
