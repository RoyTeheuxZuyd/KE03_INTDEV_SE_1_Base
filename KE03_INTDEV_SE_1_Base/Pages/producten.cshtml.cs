using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class productenModel : PageModel
    {
        private readonly IProductRepository _productRepository; // Dependency Injection van de ProductRepository

        public IEnumerable<Product> Products { get; set; } = new List<Product>(); //    Eigenschap om de producten op te slaan

        public productenModel(IProductRepository productRepository) // Constructor die de ProductRepository ontvangt via Dependency Injection
        {
            _productRepository = productRepository;
        }

        public void OnGet() // Methode die wordt aangeroepen bij een GET-verzoek naar deze pagina
        {
            Products = _productRepository.GetAllProducts();
        }
    }
}