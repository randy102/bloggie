using System.ComponentModel.DataAnnotations;
using System;

namespace Bloggie.Models {
  public class Post {
    public int Id { get; set; }

    public int AuthorId {get; set;}
    public virtual User Author {get; set;}

    public int CategoryId {get; set;}
    public virtual Category Category {get; set;}

    public string Title {get; set;}
    public string Img {get; set;}
    public string Content {get; set;}
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt {get; set;}
    public PostState State {get; set;}
  }

  public enum PostState {
    Draft,
    Pending,
    Rejected,
    Published,
    Unpublished
  }
}