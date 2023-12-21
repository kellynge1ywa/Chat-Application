using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommentsService;

public class CommentImages
{
    [Key]
    public Guid ImageId {get;set;}
    public string Image {get;set;}=string.Empty;
    [ForeignKey("CommentId")]

    public Comment Comment {get;set;} =default!;
    public Guid CommentId {get;set;}

}
