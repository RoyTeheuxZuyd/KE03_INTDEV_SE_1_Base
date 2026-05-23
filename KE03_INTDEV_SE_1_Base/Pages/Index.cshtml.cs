using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public IEnumerable<Product> LatestProducts { get; set; } = new List<Product>();

        public IndexModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void OnGet() //display new products for customers
        {
            LatestProducts = _productRepository
                .GetAllProducts()
                .OrderByDescending(p => p.Id)
                .Take(3);
        }
    }
}