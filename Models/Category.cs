using System.Collections.Generic;

namespace Bloggie.Models {
  public class Category {
    public int Id { get; set; }
    public string Name {get; set; }

    public virtual List<Post> Posts {get; set;} = new List<Post>();
  }
}