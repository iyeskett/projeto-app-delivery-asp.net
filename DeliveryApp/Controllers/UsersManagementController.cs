using DeliveryApp.Connections.Database;
using DeliveryApp.Helper;
using DeliveryApp.Models;
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

        public IActionResult New()
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");


            return View();
        }

        [HttpPost]
        public IActionResult New(UserLogin user)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                _usersTable.NewUser(user);

                return RedirectToAction("Index");
            }

            return View(user);

        }

        public IActionResult Edit(int id)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");


            var userDB = _usersTable.GetUserId(id);

            if (userDB == null) return RedirectToAction("Index");

            return View(userDB);
        }

        [HttpPost]
        public IActionResult Edit(UserLogin user)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");


            var userDB = _usersTable.GetUserId(user.Id);

            if (userDB == null) return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                userDB.Username = user.Username;
                userDB.Email = user.Email;
                userDB.Password = user.Password;
                userDB.Type = user.Type;

                _usersTable.EditUser(userDB);

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public IActionResult Details(int id)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            var userDB = _usersTable.GetUserId(id);

            if (userDB == null) return RedirectToAction("Index");

            return View(userDB);

        }

        public IActionResult Delete(int id)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");

            var userDB = _usersTable.GetUserId(id);

            if (userDB == null) return RedirectToAction("Index");

            return View(userDB);
        }

        [HttpPost]
        public IActionResult Delete(UserLogin user)
        {
            if (_session.SearchUserSession() == null) return RedirectToAction("Index", "Login");


            var userDB = _usersTable.GetUserId(user.Id);

            if (userDB == null) return RedirectToAction("Index");

            _usersTable.DeleteUser(userDB.Id);

            return RedirectToAction("Index");
        }
    }
}
