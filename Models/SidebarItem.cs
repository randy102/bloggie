
namespace Bloggie.Models {
  public class SidebarItem {
    public string Url { get; set; }
    public string Title { get; set; }
    public string Icon { get; set; }
    public bool Active { get; set; }

    public SidebarItem(string url, string title, string icon) {
      Url = url;
      Title = title;
      Icon = icon;
      Active = false;
    }
  }
}
