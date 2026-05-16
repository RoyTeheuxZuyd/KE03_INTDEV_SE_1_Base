using DataAccessLayer.Models;

namespace KE03_INTDEV_SE_1_Base.Models
{
    public class CartItem
    {
        public required Product Product { get; set; }

        public int Quantity { get; set; }
    }
}