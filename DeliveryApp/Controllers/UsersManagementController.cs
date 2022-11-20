using DeliveryApp.Connections.Database;
using DeliveryApp.Helper;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    public class UsersManagementController : Controller
    {
        private UsersTable _usersTable = new UsersTable();
        private readonly ISessionUser _session;

        public UsersManagementController(ISessionUser session)
        {
            _session = session;
        }

        public IActionResult Index()
        {

            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");


            var users = _usersTable.GetUsers();
            return View(users);
        }
    }
}
