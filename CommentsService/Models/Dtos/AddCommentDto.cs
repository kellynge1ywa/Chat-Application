namespace CommentsService;

public class AddCommentDto
{
    public string Content {get;set;} =string.Empty;
   
    public Guid PostId {get;set;}

}
