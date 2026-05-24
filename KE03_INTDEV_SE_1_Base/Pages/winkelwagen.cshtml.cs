using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KE03_INTDEV_SE_1_Base.Services;
using KE03_INTDEV_SE_1_Base.Models;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class winkelwagenModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly IOrderRepository _orderRepository;

        public List<CartItem> CartItems { get; set; } = new();

        public winkelwagenModel(CartService cartService, IOrderRepository orderRepository)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
        }

        private void LoadCart()
        {
            CartItems = _cartService.GetItems();
        }

        public void OnGet()
        {
            LoadCart();
        }

        public IActionResult OnPostRemove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToPage();
        }

        public IActionResult OnPostIncrease(int id)
        {
            _cartService.IncreaseQuantity(id);
            return RedirectToPage();
        }

        public IActionResult OnPostDecrease(int id)
        {
            _cartService.DecreaseQuantity(id);
            return RedirectToPage();
        }

        public IActionResult OnPostClear()
        {
            _cartService.Clear();
            return RedirectToPage();
        }

        public IActionResult OnPostCheckout()
        {
            var cartItems = _cartService.GetItems();

            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToPage();
            }

            var order = new Order
            {
                OrderDate = DateTime.Now
            };

            foreach (var item in cartItems)
            {
                order.OrderLines.Add(new OrderLine
                {
                    Product = item.Product,
                    ProductId = item.Product.Id,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                });
            }

            _orderRepository.AddOrder(order);

            _cartService.Clear();

            return RedirectToPage("/CheckoutSuccess");
        }
    }
}