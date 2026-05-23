using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class BestellingenModel : PageModel
    {
        private readonly IOrderRepository _orderRepository;

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();

        public BestellingenModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void OnGet()
        {
            Orders = _orderRepository.GetAllOrders().OrderByDescending(o => o.OrderDate);
        }
    }
}