using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace CommentsService;
[Route("api/[controller]")]
[ApiController]
public class ImagesController:ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly IComment _IComment;
    private readonly IcommentImage _ICommentImage;
    private readonly ResponseDto _responseDto;
    public ImagesController(IMapper mapper,IComment comment, IcommentImage icommentImage)
    {
        _IMapper=mapper;
        _IComment=comment;
        _ICommentImage=icommentImage;
        _responseDto= new ResponseDto();
    }
    [HttpPost("{Id}")]
    public async Task<ActionResult<ResponseDto>> AddImages(Guid Id, AddImageDto addImageDto){
        var comment =await  _IComment.GetComment(Id);
        if(comment ==null){
            _responseDto.Error="Comment not found";
            return NotFound(_responseDto);
        }
        //New Image
        var Image= _IMapper.Map<CommentImages>(addImageDto);
        var response=await _ICommentImage.AddImage(Id,Image);
        _responseDto.Result=response;
        return Created("",_responseDto);

    }

}
