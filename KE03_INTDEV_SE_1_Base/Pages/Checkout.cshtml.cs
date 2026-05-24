using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;
using KE03_INTDEV_SE_1_Base.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public CheckoutModel(
            CartService cartService,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _cartService = cartService;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [BindProperty] public string FirstName { get; set; }

        [BindProperty] public string LastName { get; set; }

        [BindProperty] public string Street { get; set; }

        [BindProperty] public string StreetNumber { get; set; }

        [BindProperty] public string City { get; set; }

        [BindProperty] public string PostalCode { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var cartItems = _cartService.GetItems();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToPage("/Winkelwagen");
            }

            // Optional backup: keep TempData (not strictly needed if we pass params)
            TempData["FirstName"] = FirstName;
            TempData["LastName"] = LastName;
            TempData["Street"] = Street;
            TempData["StreetNumber"] = StreetNumber;
            TempData["PostalCode"] = PostalCode;
            TempData["City"] = City;

            // Redirect to DemoPayment and pass the customer data as route/query parameters
            return RedirectToPage("/DemoPayment", new
            {
                firstName = FirstName,
                lastName = LastName,
                street = Street,
                streetNumber = StreetNumber,
                postalCode = PostalCode,
                city = City
            });
        }
    }
}