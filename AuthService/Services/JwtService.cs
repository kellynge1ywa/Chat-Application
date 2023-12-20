
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace AuthService;

public class JwtService : IJwt
{
    private readonly JwtOptions _jwtOptions;

    public JwtService(IOptions<JwtOptions> options)
    {
        _jwtOptions=options.Value;
        
    }
    public string GenerateToken(ChatUser chatUser, IEnumerable<string> Roles)
    {
         var mykey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var cred= new SigningCredentials(mykey, SecurityAlgorithms.HmacSha256);

        //payload
        List<Claim> claims=
        [
            new Claim(JwtRegisteredClaimNames.Sub, chatUser.Id.ToString()),
            //Adding a list of roles
            .. Roles.Select(k=>new Claim(ClaimTypes.Role,k)),
        ];
         //token
        var tokendescriptor=new SecurityTokenDescriptor(){
            Issuer=_jwtOptions.Issuer,
            Audience=_jwtOptions.Audience,
            Expires= DateTime.UtcNow.AddHours(4),
            Subject= new ClaimsIdentity(claims),
            SigningCredentials=cred
        };

        var token= new JwtSecurityTokenHandler().CreateToken(tokendescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
       
        
}
}
