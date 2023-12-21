namespace CommentsService;

public class Comment
{
    public Guid CommentId {get;set;}
    public string Content {get;set;} =string.Empty;
    public List<CommentImages>? Images {get;set;}

    public Guid PostId {get;set;}
    public int Replies {get;set;}
    public int Likes {get;set;}


}
