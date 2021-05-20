using Microsoft.EntityFrameworkCore;
using PerformanceDbApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Data
{
    public class Repository<T> : IRepository<T> where T : AbstractDbContext
    {
        protected readonly T _context;

        public Repository(T context)
        {
            _context = context;
        }

        // read
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public IEnumerable<Order> GetOrdersWithItems()
        {
            return _context.Orders.Include(o => o.OrderItems).ToList();
        }

        public IEnumerable<Order> GetOrdersWithItemsAndProducts()
        {
            return _context.Orders.Include(o => o.OrderItems).ThenInclude(i => i.Product).ToList();
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order GetOrderByIdIncludeOrderItems(int id)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order GetOrderByIdIncludeOrderItemsAndProducts(int id)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(o => o.Id == id);
        }

        // create

        public Product CreateProduct(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
            return product;
        }

        public Order CreateOrder(Order order)
        {
            _context.Add(order);
            _context.SaveChanges();
            return order;
        }

        public Order CreateOrderWithItems(Order order, ICollection<Product> products)
        {
            _context.Add(order);
            foreach (var item in products)
            {
                int amount = 1;
                OrderItem orderItem = new OrderItem()
                {
                    Amount = amount,
                    Price = amount * item.Price,
                    Product = item
                };
                _context.Add(orderItem);
            }

            _context.SaveChanges();
            return order;
        }


        // delete

        public void DeleteProduct(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
        }

        public void DeleteOrderItem(OrderItem orderItem)
        {
            _context.Remove(orderItem);
            _context.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _context.Remove(order);
            _context.SaveChanges();
        }

        // update
        public Product UpdateProduct(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }

        public OrderItem UpdateOrderItem(OrderItem orderItem)
        {
            _context.Update(orderItem);
            _context.SaveChanges();
            return orderItem;
        }

        public Order UpdateOrder(Order order)
        {
            _context.Update(order);
            _context.SaveChanges();
            return order;
        }
    }
}
