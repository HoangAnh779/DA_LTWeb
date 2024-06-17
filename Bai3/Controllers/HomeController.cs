using Bai3.Models;
using Bai3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace WebsiteBanHang.Controllers
{
    public class HomeController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public HomeController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m =>m.Order).ToListAsync();
            var slides = await _context.Sliders.Where(m => m.Hide == 0).OrderBy(m =>m.Order).ToListAsync();

            var viewModel = new HomeViewModel
            {
                Menus = menus,
                Sliders = slides,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _SlidePartial()
        {
            return PartialView();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
