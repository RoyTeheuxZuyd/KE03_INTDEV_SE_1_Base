using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class producteninfoModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public Product? Product { get; set; }

        public producteninfoModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
    }
}