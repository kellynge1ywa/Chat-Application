namespace AuthService;

public class ResponseDto
{
    public string Error {get;set;} =string.Empty;
    public Object Result {get;set;} =default!;
    public bool Success {get;set;}= true;

}
