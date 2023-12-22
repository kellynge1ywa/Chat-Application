﻿using System.ComponentModel.DataAnnotations;

namespace PostsService;

public class Post
{
    [Key]
    public Guid PostId {get;set;}
    public string? Content {get;set;}
    public string? Image {get;set;}
    public DateTime Time {get;set;}
     public Guid Id {get;set;}

}
