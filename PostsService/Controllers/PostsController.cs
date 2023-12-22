using System.Security.Claims;
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
    private readonly Iuser _Iuser;
    private readonly ResponseDto _responseDto;

    public PostsController(IMapper mapper, Ipost post,Iuser Iuser)
    {
        _IMapper=mapper;
        _IPost=post;
        _Iuser=Iuser;
        _responseDto=new ResponseDto();
    }
    [HttpGet]
    [Authorize]
    
    public async Task<ActionResult<List<Post>>> GetAllPosts(){
        var AllPosts= await _IPost.GetAllPosts();
        _responseDto.Result=AllPosts;
        return Ok(_responseDto);

    }
    [HttpGet("Userposts")]
    [Authorize]
    
    public async Task<ActionResult<List<UserPostDto>>> GetAllPostsByUserId(){
        var UserId=User.Claims.FirstOrDefault(claim=>claim.Type==ClaimTypes.NameIdentifier)?.Value;
        
        if(UserId==null){
            _responseDto.Error="Posts with userId not found";
            return NotFound(_responseDto);

        }
        var userPosts= await  _IPost.GetAllPosts();
        _responseDto.Result=userPosts;
        return Ok(_responseDto);

    }

    [HttpGet("{Id}")]
    [Authorize]
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
    [Authorize]
    public async Task<ActionResult<string>> CreatePost(AddPostDto NewPost){
        var Id=User.Claims.FirstOrDefault(claim=>claim.Type==ClaimTypes.NameIdentifier)?.Value;

        var newPost= _IMapper.Map<Post>(NewPost);
        newPost.Id= new Guid(Id);
        
        // if(user==null){
        //     _responseDto.Error="User not found";
        //     return NotFound(_responseDto);
        // }
        var response =await _IPost.CreatePost(newPost);
        _responseDto.Result=response;
        return Created("",_responseDto);

    }
    [HttpPut("{Id}")]
    [Authorize]

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
    [Authorize]
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
