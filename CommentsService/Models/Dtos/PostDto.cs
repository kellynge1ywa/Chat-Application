namespace CommentsService;

public class PostDto
{
    
    public Guid PostId {get;set;}
    public string? Content {get;set;}
    public string? Image {get;set;}
    public DateTime Time {get;set;}

}
