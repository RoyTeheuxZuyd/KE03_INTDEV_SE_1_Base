using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class productenModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly CartService _cartService;

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        public productenModel(
            IProductRepository productRepository,
            CartService cartService)
        {
            _productRepository = productRepository;
            _cartService = cartService;
        }

        public void OnGet()
        {
            Products = _productRepository.GetAllProducts();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product != null)
            {
                _cartService.AddToCart(product);
            }

            return RedirectToPage();
        }
    }
}