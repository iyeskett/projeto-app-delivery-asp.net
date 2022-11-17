using DeliveryApp.Helper;
using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class AppAdminController : Controller
    {
        private readonly ISessionUser _session;

        public AppAdminController(ISessionUser session)
        {
            _session = session;
        }

        public IActionResult Index()
        {
            var loggedUser = _session.SearchUserSession();
            if (loggedUser == null) return RedirectToAction("Index", "Login");

            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
