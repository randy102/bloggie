namespace Bloggie.Models {
  public class SidebarDropdown {
    public string BaseUrl { get; set; }
    public string Label { get; set; }
    public SidebarItem[] SidebarItems { get; set; }

    public SidebarDropdown(string baseUrl, string label, SidebarItem[] sidebarItems) {
      BaseUrl = baseUrl;
      Label = label;
      SidebarItems = sidebarItems;
    }
  }
}