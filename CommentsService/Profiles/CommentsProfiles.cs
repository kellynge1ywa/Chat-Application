using AutoMapper;

namespace CommentsService;

public class CommentsProfiles:Profile
{
    public CommentsProfiles()
    {
        CreateMap<AddCommentDto, Comment>().ReverseMap();
        CreateMap<AddImageDto, CommentImages>().ReverseMap();
        CreateMap<CommentImageResponseDto, Comment>().ReverseMap();
    }

}
