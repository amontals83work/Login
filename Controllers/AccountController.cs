using Login.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Login.Controllers
{
    public class AccountController : Controller
    {
        private readonly DemoContext _context;
        private readonly Tools _tools;

        public AccountController(DemoContext context)
        {
            _context = context;
            _tools = new Tools(context);
        }

        [HttpPost]
        public async Task<IActionResult> Signin(Usuario model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new Usuario
                {
                    Nombre = model.Nombre,
                    Apellidos = model.Apellidos,
                    Email = model.Email,
                    Password = model.Password,
                };

                _context.Usuarios.Add(newUser);
                await _context.SaveChangesAsync();

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, newUser.Email),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Menu", "Account");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            try
            {
                if (_tools.ValidaUsuario(model) > 0)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Email),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("Menu", "Account");
                }
            }
            catch
            {
                Exception e;
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Menu()
        {
            return View();
        }
    }
}
