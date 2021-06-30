using PerformanceDbApp.Models;
using System.Collections.Generic;

namespace PerformanceDbApp.Data
{
    public interface IRepository<T> where T : AbstractDbContext
    {
        IEnumerable<int> GetOrdersIds();
        IEnumerable<int> GetOrdersItemsIds();
        IEnumerable<int> GetProductsIds();
        IEnumerable<string> GetOrderNumbers();
        Order CreateOrder(Order order);
        Order CreateOrderWithItems(Order order, ICollection<Product> products);
        Product CreateProduct(Product product);
        void DeleteOrder(Order order);
        void DeleteOrderItem(OrderItem orderItem);
        void DeleteProduct(Product product);
        Order GetOrderById(int id);
        Order GetOrderByIdIncludeOrderItems(int id);
        Order GetOrderByIdIncludeOrderItemsAndProducts(int id);
        public OrderItem GetOrderItemById(int id);
        IEnumerable<OrderItem> GetOrderItems();
        IEnumerable<Order> GetOrders();
        IEnumerable<Order> GetOrdersWithItems();
        IEnumerable<Order> GetOrdersWithItemsAndProducts();
        Product GetProductById(int id);
        IEnumerable<Product> GetProducts();
        Order UpdateOrder(Order order);
        OrderItem UpdateOrderItem(OrderItem orderItem);
        Product UpdateProduct(Product product);
        Order GetOrderByNumber(string number);
        Order GetOrderByNumberIncludeOrderItems(string number);
        Order GetOrderByNumberIncludeOrderItemsAndProducts(string number);
    }
}