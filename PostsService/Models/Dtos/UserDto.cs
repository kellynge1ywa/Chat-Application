namespace PostsService;

public class ChatUserDto
{
    public Guid Id {get;set;}
    public string Fullname {get;set;} =string.Empty;
    
    public string Email {get;set;} =string.Empty;
    
    public string PhoneNumber {get;set;} =string.Empty;

}
