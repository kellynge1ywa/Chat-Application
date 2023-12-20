namespace AuthService;

public interface Iuser
{
    Task<string> SignUp(RegisterUserDto newUser);

    Task<LoginResponseDto> SignIn(LoginDto UserLogin);
    Task<bool> GiveRole (string Email, string RoleName);

}
