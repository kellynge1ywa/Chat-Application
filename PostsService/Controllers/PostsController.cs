using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PostsService;
[Route("api/[controller]")]
[ApiController]
public class PostsController:ControllerBase
{
    private readonly IMapper _IMapper;
    private readonly Ipost _IPost;
    private readonly ResponseDto _responseDto;

    public PostsController(IMapper mapper, Ipost post)
    {
        _IMapper=mapper;
        _IPost=post;
        _responseDto=new ResponseDto();
    }
    [HttpGet]
    [Authorize(Roles ="User")]
    
    public async Task<ActionResult<List<Post>>> GetAllPosts(){
        var AllPosts= await _IPost.GetAllPosts();
        _responseDto.Result=AllPosts;
        return Ok(_responseDto);

    }

    [HttpGet("{Id}")]
    [Authorize(Roles ="User")]
    public async Task<ActionResult<Post>> GetOnePost(Guid Id){
        var onePost=await _IPost.GetOnePost(Id);
        if(onePost==null){
            _responseDto.Error="Post not found";
            return NotFound(_responseDto);
        }
        _responseDto.Result=onePost;
        return Ok(_responseDto);
    }
    [HttpPost]
    [Authorize(Roles ="User")]
    public async Task<ActionResult<string>> CreatePost(AddPostDto NewPost){
        var newPost= _IMapper.Map<Post>(NewPost);
        var response =await _IPost.CreatePost(newPost);
        _responseDto.Result=response;
        return Created("",_responseDto);

    }
    [HttpPut("{Id}")]
    [Authorize(Roles ="User")]

    public async Task<ActionResult<string>> UpdatePost(Guid Id, AddPostDto UptPost){
        var Post= await _IPost.GetOnePost(Id);
        if(Post ==null){
            _responseDto.Result="Post not found";
            _responseDto.Success=false;
            return NotFound(_responseDto);
        }
        _IMapper.Map(UptPost,Post);
        var response=await _IPost.UpdatePost();
        _responseDto.Result=response;
        return Ok(_responseDto);
    }
    [HttpDelete("{Id}")]
     public async Task<ActionResult<Post>> DeletePost(Guid Id){
        var onePost=await _IPost.GetOnePost(Id);
        if(onePost==null){
            _responseDto.Error="Post not found";
            _responseDto.Success=false;
            return NotFound(_responseDto);
        }
        var res= await _IPost.DeletePost(onePost);
        _responseDto.Result=res;
        return Ok(_responseDto);
    }

}
