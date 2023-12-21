using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CommentsService;
[Route("Api/[controller]")]
[ApiController]
public class CommentsController:ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly IComment _IComment;
    private readonly IPosts _IPosts;
    private readonly ResponseDto _responseDto;
    public CommentsController(IMapper mapper,IComment comment,IPosts posts)
    {
        _IMapper=mapper;
        _IComment=comment;
        _IPosts=posts;
        _responseDto= new ResponseDto();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDto>> AddComment(AddCommentDto newComment){
        var comment =_IMapper.Map<Comment>(newComment);
        var post=await  _IPosts.GetPost(newComment.PostId);
        if(post.Content==null){
            _responseDto.Error="Post not found";
            return NotFound(_responseDto);
        }
        //Add comment
        var response= await _IComment.AddComent(comment);
        _responseDto.Result=response;
        return Ok(_responseDto);

    }

}
