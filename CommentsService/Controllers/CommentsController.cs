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
    
    [Authorize]
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
    [HttpGet("{PostId}")]
    [Authorize]
    public async Task<ActionResult<List<CommentImageResponseDto>>> GetAllComments(Guid PostId){
        var comment= await _IComment.GetComments(PostId);
        if(comment== null){
            _responseDto.Error="Comments for the post not found";
            return NotFound(_responseDto);
        }
        var allComments=_IMapper.Map<List<CommentImageResponseDto>>(comment);
        _responseDto.Result=allComments;
        return Ok(_responseDto);
    }
    [HttpGet("comment/{Id}")]
    [Authorize]
    public async Task<ActionResult<Comment>> GetOneComment(Guid Id){
        var comment= await _IComment.GetComment(Id);
        _responseDto.Result=comment;
        return Ok(_responseDto);
    }
    [HttpPut("{Id}")]
    [Authorize]
    public async Task<ActionResult<ResponseDto>> UpdateComment(Guid Id,AddCommentDto updateComment){
        var Comment= await _IComment.GetComment(Id);
        if(Comment== null){
            _responseDto.Error="Comment not found";
            return NotFound(_responseDto);
        }
        _IMapper.Map(updateComment,Comment);
        var response= await _IComment.UpdateComment();
        _responseDto.Result=response;
        return Ok(_responseDto);
    }
    [HttpDelete("{Id}")]
    [Authorize]
    public async Task<ActionResult<ResponseDto>> DeleteComment(Guid Id){
        var Comment= await _IComment.GetComment(Id);
        if(Comment== null){
            _responseDto.Error="Comment not found";
            return NotFound(_responseDto);
        }
        var deleted= await _IComment.DeleteComment(Comment);
        _responseDto.Result=deleted;
        return Ok(_responseDto);

    }


}
