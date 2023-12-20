using AutoMapper;

namespace AuthService;

public class UserProfiles:Profile
{
    public UserProfiles()
    {
        CreateMap<RegisterUserDto, ChatUser>().ForMember(dest=>dest.UserName, src=>src.MapFrom(g=>g.Email));
        CreateMap<ChatUserDto, ChatUser>().ReverseMap();
    }

}
