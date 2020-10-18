using Microsoft.AspNetCore.Mvc;
using Bloggie.Models;

namespace Bloggie.Pages.Components.SidebarItemComponent {

  public class SidebarItemComponent : ViewComponent {

    public SidebarItemComponent() { }

    public IViewComponentResult Invoke(SidebarItem sidebarItem, bool active) {
      sidebarItem.Active = active;
      return View("Default", sidebarItem);
    }

  }

}