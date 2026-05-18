using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public IActionResult OnPostAddToCart(int id, int quantity)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            for (int i = 0; i < quantity; i++)
            {
                _cartService.AddToCart(product);
            }

            return RedirectToPage(new { id });
        }

        public IActionResult OnPostRemoveFromCart(int id)
        {
            _cartService.RemoveFromCart(id);

            return RedirectToPage(new { id });
        }
    }
}