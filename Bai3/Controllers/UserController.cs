using Bai3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bai3.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
namespace WebsiteBanHang.Controllers
{
    public class UserController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public UserController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var viewModel = new UserViewModel
            {
                Menus = menus,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial(){ return PartialView(); }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var viewModel = new UserViewModel
            {
                Menus = menus,
                Register = model.Register,
            };
            if (model.Register != null)
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username ==
                model.Register.Username);
                if (existingUser != null)
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại.";
                    return View(viewModel);
                }
                model.Register.Password =
                BCrypt.Net.BCrypt.HashPassword(model.Register.Password);
                model.Register.Permission = 0;
                model.Register.Hide = 0;
                _context.Users.Add(model.Register);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "User");
            }
            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).ToListAsync();
            var viewModel = new UserViewModel
            {
                Menus = menus,
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).ToListAsync();
            var viewModel = new UserViewModel
            {
                Menus = menus,
                Register = model.Register,
            };
            if (model.Register != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username ==
                model.Register.Username);
                if (user != null && BCrypt.Net.BCrypt.Verify(model.Register.Password,
                user.Password))
                {
                    var claims = new List<Claim>
                    {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Permission.ToString()),
                    };
                    var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                    };
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }
        public async Task<IActionResult> Info()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>
            m.Order).ToListAsync();
            var users = new User();
            if (User.Identity.IsAuthenticated)
            {
                string username = User.Identity.Name;
                if (username != null)
                {
                    users = await _context.Users.FirstOrDefaultAsync(m => m.Username == username);
                }
            }
            var viewModel = new UserViewModel
            {
                Menus = menus,
                Register = users,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}