using System.ComponentModel.DataAnnotations;

namespace AuthService;

public class RegisterUserDto
{
    [Required]
    public string Fullname {get;set;} =string.Empty;
    [Required]
    public string Email {get;set;} =string.Empty;
    [Required]
    public string PhoneNumber {get;set;} =string.Empty;
    [Required]
    public string Password {get;set;} =string.Empty;
    public string ? Roles  {get;set;} ="User";


}
