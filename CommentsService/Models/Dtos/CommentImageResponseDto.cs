namespace CommentsService;

public class CommentImageResponseDto
{
     public Guid Id {get;set;}
    public string Content {get;set;} =string.Empty;
    
    public List<AddImageDto> Images {get;set;}= new List<AddImageDto>();
    public Guid? PostId {get;set;}
    public int Replies {get;set;}
    public int Likes {get;set;}
    

}
