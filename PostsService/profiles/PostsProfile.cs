using AutoMapper;

namespace PostsService;

public class PostsProfile:Profile
{
    public PostsProfile()
    {
        CreateMap<AddPostDto,Post>().ReverseMap();
        
    }

}
