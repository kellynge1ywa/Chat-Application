using Microsoft.AspNetCore.Identity;

namespace AuthService;

public class ChatUser:IdentityUser //Extends identity user
{
    public string Fullname {get;set;} = string.Empty;

}
