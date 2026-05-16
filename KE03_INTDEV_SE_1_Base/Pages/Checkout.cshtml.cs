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

        [BindProperty]
        public string FirstName { get; set; }

        [BindProperty]
        public string LastName { get; set; }

        [BindProperty]
        public string Street { get; set; }

        [BindProperty]
        public string StreetNumber { get; set; }

        [BindProperty]
        public string City { get; set; }

        [BindProperty]
        public string PostalCode { get; set; }

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

            var fullName = $"{FirstName} {LastName}";
            var fullAddress = $"{Street} {StreetNumber}, {PostalCode} {City}";

            var customer = _customerRepository
                .GetAllCustomers()
                .FirstOrDefault(c => c.Name == fullName && c.Address == fullAddress);

            if (customer == null)
            {
                customer = new Customer
                {
                    Name = fullName,
                    Address = fullAddress
                };

                _customerRepository.AddCustomer(customer);
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                Customer = customer
            };

            foreach (var item in cartItems)
            {
                var product = _productRepository.GetProductById(item.Product.Id);

                if (product != null)
                {
                    order.Products.Add(product);
                }
            }

            _orderRepository.AddOrder(order);

            _cartService.Clear();

            return RedirectToPage("/Index");
        }
    }
}