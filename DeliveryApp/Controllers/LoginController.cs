using DeliveryApp.Models;
using Microsoft.AspNetCore.Mvc;
using DeliveryApp.Helper;
using DeliveryApp.Models;
using DeliveryApp.Connections.Database;

namespace DeliveryApp.Controllers
{
    public class LoginController : Controller
    {
        private UsersTable _usersTable = new UsersTable();
        private readonly ISessionUser _session;

        public LoginController( ISessionUser session)
        {
            _session = session;
        }
        
        public IActionResult Index()
        {
            // Se o usuario estiver logado, redireciona para a Home
            if (_session.SearchUserSession() != null) return RedirectToAction("Index", "AppAdmin");
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Exit()
        {
            _session.RemoveUserSession();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public IActionResult Login(UserLogin user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Email))
                {
                    UserLogin loginBanco = _usersTable.GetUser(user.Email);

                    if (loginBanco.Email == user.Email)
                    {
                        if (loginBanco.Password == user.Password)
                        {
                            _session.CreateUserSession(loginBanco);
                            return RedirectToAction("Index", "AppAdmin");
                        }
                        TempData["MensagemErro"] = $"Senha do usuário inválida, tente novamente.";
                    }
                    else
                        TempData["MensagemErro"] = $"Email e/ou senha inválido(s). Por favor, tente novamente";
                }
                return View("Index");

            }
            catch (Exception e)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos realizar o seu login, tente novamente. Detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
