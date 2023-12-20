using System.ComponentModel.DataAnnotations;

namespace PostsService;

public class AddPostDto
{
    [Required]
    public string? Content {get;set;}
    [Required]
    public string? Image {get;set;}
    public DateTime Time {get;set;}

}
