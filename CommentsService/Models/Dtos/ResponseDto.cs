namespace CommentsService;

public class ResponseDto
{
    public object Result {get;set;}= default!;
    public string Error {get;set;} =string.Empty;
    public bool Success {get;set;}=true;

}
