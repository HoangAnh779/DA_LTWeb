using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bai3.Models;
using Bai3.ViewModels;

namespace Bai3.Controllers
{
    public class ProductController : Controller
    {
        private readonly WebsiteBanHangContext _context;

        public ProductController(WebsiteBanHangContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var prods = await _context.Products.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();

            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> Index2()
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var slides = await _context.Sliders.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var daxay_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 2).OrderBy(m => m.Order).Take(4).ToListAsync();
            var daxay_cate_prods = await _context.Catologies.Where(m => m.IdCat == 2).FirstOrDefaultAsync();
            var tra_prods = await _context.Products.Where(m => m.Hide == 0 && m.IdCat == 1).OrderBy(m => m.Order).Take(4).ToListAsync();
            var tra_cate_prods = await _context.Catologies.Where(m => m.IdCat == 1).FirstOrDefaultAsync();
            var trahop_prods = await _context.Products.Where(m => m.Hide == 1 && m.IdCat == 3).OrderBy(m => m.Order).Take(4).ToListAsync();
            var trahop_cate_prods = await _context.Catologies.Where(m => m.IdCat == 3).FirstOrDefaultAsync();
            var tragoi_prods = await _context.Products.Where(m => m.Hide == 1 && m.IdCat == 4).OrderBy(m => m.Order).Take(4).ToListAsync();
            var tragoi_cate_prods = await _context.Catologies.Where(m => m.IdCat == 4).FirstOrDefaultAsync();
            var tralon_prods = await _context.Products.Where(m => m.Hide == 1 && m.IdCat == 5).OrderBy(m => m.Order).Take(4).ToListAsync();
            var tralon_cate_prods = await _context.Catologies.Where(m => m.IdCat == 5).FirstOrDefaultAsync();

            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Sliders = slides,
                TraProds = tra_prods,
                DaxayProds = daxay_prods,
                TrahopProds = trahop_prods,
                TragoiProds = tragoi_prods,
                TralonProds = tralon_prods,
                TraCateProds = tra_cate_prods,
                DaxayCateProds = daxay_cate_prods,
                TrahopCateProds = trahop_cate_prods,
                TragoiCateProds = tragoi_cate_prods,
                TralonCateProds = tralon_cate_prods,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _MenuPartial() { return PartialView(); }
        public async Task<IActionResult> _ProductPartial()
        {
            return PartialView();
        }

        public async Task<IActionResult> CateProd(string slug, long id)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();

            var cateProds = await _context.Catologies.Where(cp => cp.IdCat == id && cp.Link == slug).FirstOrDefaultAsync();
            if(cateProds == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "CateProd Error",
                };
                return View("Error", errorViewModel);
            }

            var prods = await _context.Products.Where(m => m.IdCat == cateProds.IdCat).OrderBy(m => m.Order).ToListAsync();

            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
                cateName = cateProds.NameCat,
            };
            return View(viewModel);
        }
        public async Task<IActionResult> _SlidePartial()
        {
            return PartialView();
        }

        public async Task<IActionResult> ProdDetail(string slug, long id)
        {
            var menus = await _context.Menus.Where(m => m.Hide == 0).OrderBy(m => m.Order).ToListAsync();
            var prods = await _context.Products.Where(m => m.Link == slug && m.IdPro == id).ToListAsync();
            
            if (prods == null)
            {
                var errorViewModel = new ErrorViewModel
                {
                    RequestId = "Product Error",
                };
                return View("Error", errorViewModel);
            }

            var viewModel = new ProductViewModel
            {
                Menus = menus,
                Prods = prods,
            };
            return View(viewModel);
        }
    }
}
