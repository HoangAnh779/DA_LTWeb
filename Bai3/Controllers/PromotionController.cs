using Bai3.Models;
using Bai3.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Bai3.Controllers
{
    public class PromotionController : Controller
    {
        private readonly WebsiteBanHangContext _context;
        public PromotionController(WebsiteBanHangContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var viewModel = new PromotionViewModel
            {
                Menus = menus,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial()
        {
            return PartialView();
        }

    }
}
