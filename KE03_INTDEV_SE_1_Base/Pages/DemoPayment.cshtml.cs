using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;
using KE03_INTDEV_SE_1_Base.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class DemoPaymentModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public DemoPaymentModel(
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

        public List<CartItem> CartItems { get; set; } = new();

        public decimal Subtotal { get; set; }

        // Fixed shipping cost of €2.00
        public decimal ShippingCosts { get; set; } = 2.00m;

        public decimal Total => Subtotal + ShippingCosts;

        // Customer info read from TempData
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }

        public void OnGet(string? firstName, string? lastName, string? street, string? streetNumber, string? postalCode,
            string? city)
        {
            // If query params were provided (from RedirectToPage), use them;
            // otherwise fall back to TempData (Peek) if present.
            FirstName = !string.IsNullOrWhiteSpace(firstName) ? firstName : TempData.Peek("FirstName") as string;
            LastName = !string.IsNullOrWhiteSpace(lastName) ? lastName : TempData.Peek("LastName") as string;
            Street = !string.IsNullOrWhiteSpace(street) ? street : TempData.Peek("Street") as string;
            StreetNumber = !string.IsNullOrWhiteSpace(streetNumber)
                ? streetNumber
                : TempData.Peek("StreetNumber") as string;
            PostalCode = !string.IsNullOrWhiteSpace(postalCode) ? postalCode : TempData.Peek("PostalCode") as string;
            City = !string.IsNullOrWhiteSpace(city) ? city : TempData.Peek("City") as string;

            CartItems = _cartService.GetItems() ?? new List<CartItem>();
            Subtotal = CartItems.Sum(x => x.Product.Price * x.Quantity);
        }

        public IActionResult OnPost(string method)
        {
            // Validate cart
            var cartItems = _cartService.GetItems();
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToPage("/Winkelwagen");
            }

            // Try query/TempData/form in this order (OnGet used query or TempData; POST should include hidden fields)
            string? firstName = TempData["FirstName"] as string ?? Request.Form["FirstName"].FirstOrDefault();
            string? lastName = TempData["LastName"] as string ?? Request.Form["LastName"].FirstOrDefault();
            string? street = TempData["Street"] as string ?? Request.Form["Street"].FirstOrDefault();
            string? streetNumber = TempData["StreetNumber"] as string ?? Request.Form["StreetNumber"].FirstOrDefault();
            string? postalCode = TempData["PostalCode"] as string ?? Request.Form["PostalCode"].FirstOrDefault();
            string? city = TempData["City"] as string ?? Request.Form["City"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(street))
            {
                // Missing data -> send back to checkout
                return RedirectToPage("/Checkout");
            }

            // (rest of order creation unchanged)
            var fullName = $"{firstName} {lastName}";
            var fullAddress = $"{street} {streetNumber}, {postalCode} {city}";

            var customer = _customerRepository
                .GetAllCustomers()
                .FirstOrDefault(c => c.Name == fullName && c.Address == fullAddress);

            if (customer == null)
            {
                customer = new Customer { Name = fullName, Address = fullAddress };
                _customerRepository.AddCustomer(customer);
            }

            var order = new Order { OrderDate = DateTime.Now, Customer = customer };

            foreach (var item in cartItems)
            {
                var product = _productRepository.GetProductById(item.Product.Id);
                if (product != null)
                {
                    order.OrderLines.Add(new OrderLine
                    {
                        Product = product,
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        Price = product.Price 
                    });
                }
            }

            _orderRepository.AddOrder(order);
            _cartService.Clear();

            return RedirectToPage("/CheckoutSuccess");
        }
    }
}