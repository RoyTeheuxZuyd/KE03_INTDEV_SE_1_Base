using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class producteninfoModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly CartService _cartService;

        public Product? Product { get; set; }

        public producteninfoModel(
            IProductRepository productRepository,
            CartService cartService)
        {
            _productRepository = productRepository;
            _cartService = cartService;
        }

        public IActionResult OnGet(int id)
        {
            Product = _productRepository.GetProductById(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostAddToCart(int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product != null)
            {
                _cartService.AddToCart(product);
            }

            return RedirectToPage(new { id = id });
        }
    }
}