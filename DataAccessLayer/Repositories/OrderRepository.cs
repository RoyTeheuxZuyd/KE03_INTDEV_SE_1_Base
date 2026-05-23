using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MatrixIncDbContext _context;

        public OrderRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            _context.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrders() //we need to get customer, then their products and the parts of products. Prices is also important
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                    .ThenInclude(p => p.Parts);
        }

        public Order? GetOrderById(int id) //we need to get customer, then products and the parts of the products and get them by order id.
        {
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Products)
                    .ThenInclude(p => p.Parts)
                .FirstOrDefault(o => o.Id == id);
        }

        public void UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }
    }
}