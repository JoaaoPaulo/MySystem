using Microsoft.AspNetCore.Mvc;
using ClothesStore.Data;
using ClothesStore.Models;


namespace ClothesStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Details (int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}