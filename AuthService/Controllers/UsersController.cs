using ChatMessageBus;
using Microsoft.AspNetCore.Mvc;

namespace AuthService;

[Route("api/[controller]")]
[ApiController]

public class UsersController:ControllerBase
{
    private readonly Iuser _User;
    private readonly ResponseDto _responseDto;
    private readonly IConfiguration _IConfiguration;
    public UsersController(Iuser User, IConfiguration configuration)
    {
        _User=User;
        _IConfiguration=configuration;
        _responseDto= new ResponseDto();
        
    }
    [HttpPost("Register")]
    public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto newUserDto){
        var response= await _User.SignUp(newUserDto);
        if(string.IsNullOrWhiteSpace(response)){
            _responseDto.Result="Registration successful";
            //Add message to queue

            var message= new ChatMessageDto(){
                Fullname=newUserDto.Fullname,
                Email=newUserDto.Email

            };
            var messageBody= new MessageBus();
            await messageBody.PublishMessage(message,_IConfiguration.GetValue<string>("ServiceBus:register"));
            return Created("", _responseDto);
        }
        _responseDto.Error=response;
        _responseDto.Success=false;
        return BadRequest(_responseDto);

    }

    [HttpPost("AssignRole")]
    public async Task<ActionResult<ResponseDto>> AssignRole(RegisterUserDto newUserRoleDto){
        var response= await _User.GiveRole(newUserRoleDto.Email, newUserRoleDto.Roles);
        if(response){
            _responseDto.Result=response;
            return Ok(_responseDto);
        }
        _responseDto.Error="Cannot assign";
        _responseDto.Result=response;
        _responseDto.Success=false;
        return BadRequest(_responseDto);

    }

     [HttpPost("Login")]
    public async Task<ActionResult<ResponseDto>> LoginUser(LoginDto loginDto){
        var response= await _User.SignIn(loginDto);
        if(response.User!=null){
            _responseDto.Result=response;
            return Created("", _responseDto);
        }
        _responseDto.Error="Invalid credentials";
        _responseDto.Success=false;
        return BadRequest(_responseDto);

    }

}
