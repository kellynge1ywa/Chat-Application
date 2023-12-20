
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthService;

public class UserServices : Iuser
{
    private readonly ChatDbContext _chatDbContext;
    private readonly IMapper _IMapper;
    private readonly UserManager<ChatUser> _ChatUserManager; //pass identityUser if you have no user
    private readonly RoleManager<IdentityRole> _roleManager; //pass identity role
    private readonly IJwt _IJwt;
    public UserServices(ChatDbContext chatDbContext, IMapper Imapper, UserManager<ChatUser> userManager, RoleManager<IdentityRole> roleManager, IJwt jwt)
    {
        _chatDbContext=chatDbContext;
        _IMapper=Imapper;
        _ChatUserManager=userManager;
        _roleManager=roleManager;
        _IJwt=jwt;

        
    }
    public async Task<bool> GiveRole(string Email, string RoleName)
    {
        var user= await _chatDbContext.ChatUser.Where(k=>k.Email.ToLower()==Email.ToLower()).FirstOrDefaultAsync();
        if(user == null){
            return false;
        } else{
            if(!_roleManager.RoleExistsAsync(RoleName).GetAwaiter().GetResult()){
                //create role
                await _roleManager.CreateAsync(new IdentityRole(RoleName));
            }
            await _ChatUserManager.AddToRoleAsync(user,RoleName);
            return true;
        }
    }

    public async Task<LoginResponseDto> SignIn(LoginDto UserLogin)
    {
        //check if user exist
        var user=await _chatDbContext.ChatUser.Where(k=>k.UserName.ToLower()== UserLogin.Username.ToLower()).FirstOrDefaultAsync();
        //compare hashed password with plain password
        var IsPasswordValid= _ChatUserManager.CheckPasswordAsync(user, UserLogin.Password).GetAwaiter().GetResult();
        if(user ==null || !IsPasswordValid){
            return new LoginResponseDto();
        }
        var loggedUser= _IMapper.Map<ChatUserDto>(user);
        //check user role
        var userRole=await _ChatUserManager.GetRolesAsync(user);
        var token= _IJwt.GenerateToken(user,userRole);
        var loggedInUser= new LoginResponseDto(){
            User=loggedUser,
            Token=token

        };
        return loggedInUser;
        
    }


    public async Task<string> SignUp(RegisterUserDto newUser)
    {
        try{
            var NewUser= _IMapper.Map<ChatUser>(newUser);
            var results= await _ChatUserManager.CreateAsync(NewUser, newUser.Password);
            if(results.Succeeded){
                return string.Empty;

            } else{
                return results.Errors.FirstOrDefault()!.Description;
            }
        } catch(Exception ex){
            return ex.Message;
        }
    }
}
