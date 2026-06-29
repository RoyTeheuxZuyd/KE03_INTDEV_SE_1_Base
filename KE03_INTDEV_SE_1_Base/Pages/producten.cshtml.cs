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

        public void OnGet(string? search, string? sort)
        {
            var products = _productRepository.GetAllProducts();

            if (!string.IsNullOrWhiteSpace(search))
            {
                products = products.Where(p =>
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)); //for searching in searchbar
            }

            products = sort switch //sorting 
            {
                "price_asc" => products.OrderBy(p => p.Price),
                "price_desc" => products.OrderByDescending(p => p.Price),
                "newest" => products.OrderByDescending(p => p.Id), //uses the id for oldest to newest, higher id number = newer
                "oldest" => products.OrderBy(p => p.Id),
                _ => products
            };

            Products = products;
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