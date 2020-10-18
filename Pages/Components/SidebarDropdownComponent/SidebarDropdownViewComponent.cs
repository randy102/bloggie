using Microsoft.AspNetCore.Mvc;
using Bloggie.Models;

namespace Bloggie.Pages.Components.SidebarDropdownComponent {

  public class SidebarDropdownComponent : ViewComponent {

    public SidebarDropdownComponent() { }

    public IViewComponentResult Invoke(string baseUrl, string label, SidebarItem[] sidebarItems) {
      return View("Default", new SidebarDropdown(baseUrl, label, sidebarItems));
    }

  }

}