using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Models;

namespace DeliveryApp.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string userSession = HttpContext.Session.GetString("loggedUserSession");
            if (string.IsNullOrEmpty(userSession)) return null;

            UserLogin login = JsonConvert.DeserializeObject<UserLogin>(userSession);

            return View(login);
        }
    }
}
