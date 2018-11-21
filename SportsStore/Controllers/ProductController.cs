using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.Domain;

namespace SportsStore.Controllers {
    public class ProductController : Controller {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository) {
            _productRepository = productRepository;
        }
        public IActionResult Index() {
            return View(_productRepository.GetAll());
        }
    }
}