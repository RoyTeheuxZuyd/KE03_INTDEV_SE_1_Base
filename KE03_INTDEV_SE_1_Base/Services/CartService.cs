using DataAccessLayer.Models;
using KE03_INTDEV_SE_1_Base.Models;

namespace KE03_INTDEV_SE_1_Base.Services
{
    public class CartService
    {
        private readonly List<CartItem> _items = new();

        public List<CartItem> GetItems()
        {
            return _items;
        }

        public void AddToCart(Product product)
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

        public void RemoveFromCart(int productId)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                _items.Remove(item);
            }
        }

        public void IncreaseQuantity(int productId)
        {
            var item = _items.FirstOrDefault(x => x.Product.Id == productId);

            if (item != null)
            {
                item.Quantity++;
            }
        }

        public void DecreaseQuantity(int productId)
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

        public void Clear()
        {
            _items.Clear();
        }

        public int GetTotalItems()
        {
            return _items.Sum(x => x.Quantity);
        }
    }
}