using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Models.Domain;
using SportsStore.Models.ProductViewModel;

namespace SportsStore.Controllers {
    public class ProductController : Controller {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;


        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index() {
            return View(_productRepository.GetAll());
        }

        [HttpGet]
        public IActionResult Edit(int id) {
            ViewData["Categories"] = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");
            Product product = _productRepository.GetBy(id);
            return View(new EditViewModel(product));
        }

        [HttpPost]
        public IActionResult Edit(int id, EditViewModel model) {
            Product product = null;
            product = _productRepository.GetBy(id);
            product.MapEditViewToProduct(model.Name, model.Description, model.Price, model.InStock, _categoryRepository.GetById(model.CategoryId));
            _productRepository.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Create() {
            return RedirectToAction("Edit");
        }

    }
}