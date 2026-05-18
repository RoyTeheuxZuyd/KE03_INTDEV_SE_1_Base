using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Models;

namespace KE03_INTDEV_SE_1_Base.Services
{
    public class CartService
    {
        private readonly List<CartItem> _items = new();

        public List<CartItem> GetItems() // get the shop cart items
        {
            return _items;
        }

        public void AddToCart(Product product) // add item to the shop cart
        {
            var existing = _items.FirstOrDefault(x => x.Product.Id == product.Id);

            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                _items.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
            }
        }

        public void RemoveFromCart(int productId) // remove from the shop cart
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public void IncreaseQuantity(int productId) //increase amount of item in shop cart
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                item.Quantity++;
            }
        }

        public void DecreaseQuantity(int productId) //decrease amount of item in shop cart
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    _items.Remove(item);
                }
            }
        }

        public void Clear() //clear the shop cart
        {
            _items.Clear();
        }

        public int GetTotalItems() //get total amount in the shop cart for final pricing
        {
            return _items.Sum(x => x.Quantity);
        }
    }
}