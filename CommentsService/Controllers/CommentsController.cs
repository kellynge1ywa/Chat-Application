using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles ="User")]
    public async Task<ActionResult<ResponseDto>> AddComment(AddCommentDto newComment){
        var comment =_IMapper.Map<Comment>(newComment);
        var post=await  _IPosts.GetPost(comment.PostId);
        if(string.IsNullOrWhiteSpace(post.Content)){
            _responseDto.Error="Post not found";
            return NotFound(_responseDto);
        }
        //Add comment
        var response= await _IComment.AddComent(comment);
        _responseDto.Result=response;
        return Ok(_responseDto);

    }
    [HttpGet("{Id}")]
    [Authorize(Roles ="User")]
    public async Task<ActionResult<List<CommentImageResponseDto>>> GetAllComments(Guid Id){
        var comment= await _IComment.GetComments(Id);
        if(comment== null){
            _responseDto.Error="Comments for the post not found";
            return NotFound(_responseDto);
        }
        var allComments=_IMapper.Map<List<CommentImageResponseDto>>(comment);
        _responseDto.Result=allComments;
        return Ok(_responseDto);
    }
    [HttpGet("comment/{Id}")]
    [Authorize(Roles ="User")]
    public async Task<ActionResult<Comment>> GetOneComment(Guid Id){
        var comment= await _IComment.GetComment(Id);
        _responseDto.Result=comment;
        return Ok(_responseDto);
    }

}
