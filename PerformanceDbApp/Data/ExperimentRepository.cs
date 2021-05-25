using PerformanceDbApp.Models;
using PerformanceDbApp.Utils;
using PerformanceDbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Data
{
    public class ExperimentRepository : IExperimentRepository
    {
        private readonly IRepository<PostgresDbContext> _postgresRepository;
        private readonly IRepository<MsDbContext> _msRepository;
        private Stopwatch stopwatch;
        private readonly int count = 20;

        public ExperimentRepository(IRepository<PostgresDbContext> postgresRepository, IRepository<MsDbContext> msRepository)
        {
            _postgresRepository = postgresRepository;
            _msRepository = msRepository;
            stopwatch = new Stopwatch();
        }

        // prepare data
        public void DeleteAllData()
        {
            var ordersPostgres = _postgresRepository.GetOrders();
            foreach (var item in ordersPostgres)
            {
                _postgresRepository.DeleteOrder(item);
            }

            var orderItemsPostgres = _postgresRepository.GetOrderItems();
            foreach (var item in orderItemsPostgres)
            {
                _postgresRepository.DeleteOrderItem(item);
            }

            var productsPostgres = _postgresRepository.GetProducts();
            foreach (var item in productsPostgres)
            {
                _postgresRepository.DeleteProduct(item);
            }

            var ordersMS = _msRepository.GetOrders();
            foreach (var item in ordersMS)
            {
                _msRepository.DeleteOrder(item);
            }

            var orderItemsMS = _msRepository.GetOrderItems();
            foreach (var item in orderItemsMS)
            {
                _msRepository.DeleteOrderItem(item);
            }

            var productsMS = _msRepository.GetProducts();
            foreach (var item in productsMS)
            {
                _msRepository.DeleteProduct(item);
            }
        }

        public void CreateDefaultData()
        {
            for (int i = 0; i < 1000; i++)
            {
                var order1 = new Order()
                {
                    Notes = StringGeneratorUtil.GenerateRandomString(),
                    OrderTotal = 20D
                };

                var order2 = new Order()
                {
                    Notes = StringGeneratorUtil.GenerateRandomString(),
                    OrderTotal = 20D
                };

                var product1 = new Product()
                {
                    Name = StringGeneratorUtil.GenerateRandomString(),
                    Price = 5D,
                    Type = StringGeneratorUtil.GenerateRandomString()
                };

                var product2 = new Product()
                {
                    Name = StringGeneratorUtil.GenerateRandomString(),
                    Price = 5D,
                    Type = StringGeneratorUtil.GenerateRandomString()
                };

                var product3 = new Product()
                {
                    Name = StringGeneratorUtil.GenerateRandomString(),
                    Price = 5D,
                    Type = StringGeneratorUtil.GenerateRandomString()
                };

                var product4 = new Product()
                {
                    Name = StringGeneratorUtil.GenerateRandomString(),
                    Price = 5D,
                    Type = StringGeneratorUtil.GenerateRandomString()
                };

                var prodPost1 = _postgresRepository.CreateProduct(product1);
                var prodPost2 = _postgresRepository.CreateProduct(product2);

                var prodMS1 = _msRepository.CreateProduct(product3);
                var prodMS2 = _msRepository.CreateProduct(product4);

                _postgresRepository.CreateOrderWithItems(order1, new List<Product>() { prodPost1, prodPost2 });
                _msRepository.CreateOrderWithItems(order2, new List<Product>() { prodMS1, prodMS2 });
            }
        }

        // read
        public ResultViewModel SelectAllOrders()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectAllOrders),
                PostgreSQL = SelectAllOrdersPostgres(),
                MS_SQL = SelectAllOrdersMS()
            };
            return resultViewModel;
        }

        private PartialResult SelectAllOrdersPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();
                var orders = _postgresRepository.GetOrders();
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult SelectAllOrdersMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();
                var orders = _msRepository.GetOrders();
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel SelectAllOrdersWithItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectAllOrdersWithItems),
                PostgreSQL = SelectAllOrdersWithItemsPostgres(),
                MS_SQL = SelectAllOrdersWithItemsMS()
            };
            return resultViewModel;
        }

        private PartialResult SelectAllOrdersWithItemsPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();
                var orders = _postgresRepository.GetOrdersWithItems();
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult SelectAllOrdersWithItemsMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();
                var orders = _msRepository.GetOrdersWithItems();
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel SelectAllOrdersWithItemsAndProducts()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectAllOrdersWithItemsAndProducts),
                PostgreSQL = SelectAllOrdersWithItemsAndProductsPostgres(),
                MS_SQL = SelectAllOrdersWithItemsAndProductsMS()
            };
            return resultViewModel;
        }

        private PartialResult SelectAllOrdersWithItemsAndProductsPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();
                var orders = _postgresRepository.GetOrdersWithItems();
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult SelectAllOrdersWithItemsAndProductsMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                stopwatch.Start();
                var orders = _msRepository.GetOrdersWithItems();
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel SelectOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectOrder),
                PostgreSQL = SelectOrderPostgres(),
                MS_SQL = SelectOrderMS()
            };
            return resultViewModel;
        }

        private PartialResult SelectOrderPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var ids = _postgresRepository.GetOrdersIds();
                var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                stopwatch.Start();
                var orders = _postgresRepository.GetOrderById(id);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult SelectOrderMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var ids = _msRepository.GetOrdersIds();
                var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                stopwatch.Start();
                var orders = _msRepository.GetOrderById(id);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel SelectOrderWithItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectOrderWithItems),
                PostgreSQL = SelectOrderWithItemsPostgres(),
                MS_SQL = SelectOrderWithItemsMS()
            };
            return resultViewModel;
        }

        private PartialResult SelectOrderWithItemsPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var ids = _postgresRepository.GetOrdersIds();
                var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                stopwatch.Start();
                var orders = _postgresRepository.GetOrderByIdIncludeOrderItems(id);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult SelectOrderWithItemsMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var ids = _msRepository.GetOrdersIds();
                var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                stopwatch.Start();
                var orders = _msRepository.GetOrderByIdIncludeOrderItems(id);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel SelectOrderWithItemsAndProducts()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectOrderWithItemsAndProducts),
                PostgreSQL = SelectOrderWithItemsAndProductsPostgres(),
                MS_SQL = SelectOrderWithItemsAndProductsMS()
            };
            return resultViewModel;
        }

        private PartialResult SelectOrderWithItemsAndProductsPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var ids = _postgresRepository.GetOrdersIds();
                var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                stopwatch.Start();
                var orders = _postgresRepository.GetOrderByIdIncludeOrderItemsAndProducts(id);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult SelectOrderWithItemsAndProductsMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var ids = _msRepository.GetOrdersIds();
                var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                stopwatch.Start();
                var orders = _msRepository.GetOrderByIdIncludeOrderItemsAndProducts(id);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        // create
        public ResultViewModel CreateOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(CreateOrder),
                PostgreSQL = CreateOrderPostgres(),
                MS_SQL = CreateOrderMS()
            };
            return resultViewModel;
        }

        private PartialResult CreateOrderPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var newOrder = new Order()
                {
                    Notes = StringGeneratorUtil.GenerateRandomString(),
                    OrderTotal = 10D
                };
                stopwatch.Start();
                var order = _postgresRepository.CreateOrder(newOrder);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult CreateOrderMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var newOrder = new Order()
                {
                    Notes = StringGeneratorUtil.GenerateRandomString(),
                    OrderTotal = 10D
                };
                stopwatch.Start();
                var order = _msRepository.CreateOrder(newOrder);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel CreateOrderAndItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(CreateOrderAndItems),
                PostgreSQL = CreateOrderAndItemsPostgres(),
                MS_SQL = CreateOrderAndItemsMS()
            };
            return resultViewModel;
        }

        private PartialResult CreateOrderAndItemsPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var newOrder = new Order()
                {
                    Notes = StringGeneratorUtil.GenerateRandomString(),
                    OrderTotal = 10D
                };
                var newProduct = new Product()
                {
                    Name = StringGeneratorUtil.GenerateRandomString(),
                    Price = 10D,
                    Type = StringGeneratorUtil.GenerateRandomString()
                };
                var product = _postgresRepository.CreateProduct(newProduct);

                stopwatch.Start();
                var order = _postgresRepository.CreateOrderWithItems(newOrder, new List<Product>() { product });
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult CreateOrderAndItemsMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var newOrder = new Order()
                {
                    Notes = StringGeneratorUtil.GenerateRandomString(),
                    OrderTotal = 10D
                };
                var newProduct = new Product()
                {
                    Name = StringGeneratorUtil.GenerateRandomString(),
                    Price = 10D,
                    Type = StringGeneratorUtil.GenerateRandomString()
                };
                var product = _msRepository.CreateProduct(newProduct);

                stopwatch.Start();
                var orders = _msRepository.CreateOrderWithItems(newOrder, new List<Product>() { product });
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        // delete
        public ResultViewModel DeleteOrderItem()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(DeleteOrderItem),
                PostgreSQL = DeleteOrderItemPostgres(),
                MS_SQL = DeleteOrderItemMS()
            };
            return resultViewModel;
        }

        private PartialResult DeleteOrderItemPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orderItems = _postgresRepository.GetOrderItems();
                var orderItem = orderItems.ElementAt(RandomNumberUtil.GenerateFromRange(0, orderItems.Count()));
                stopwatch.Start();
                _postgresRepository.DeleteOrderItem(orderItem);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult DeleteOrderItemMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orderItems = _msRepository.GetOrderItems();
                var orderItem = orderItems.ElementAt(RandomNumberUtil.GenerateFromRange(0, orderItems.Count()));
                stopwatch.Start();
                _msRepository.DeleteOrderItem(orderItem);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel DeleteOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(DeleteOrder),
                PostgreSQL = DeleteOrderPostgres(),
                MS_SQL = DeleteOrderMS()
            };
            return resultViewModel;
        }

        private PartialResult DeleteOrderPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orders = _postgresRepository.GetOrders();
                var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                stopwatch.Start();
                _postgresRepository.DeleteOrder(order);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult DeleteOrderMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orders = _msRepository.GetOrders();
                var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                stopwatch.Start();
                _msRepository.DeleteOrder(order);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        // update
        public ResultViewModel UpdateOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(UpdateOrder),
                PostgreSQL = UpdateOrderPostgres(),
                MS_SQL = UpdateOrderMS()
            };
            return resultViewModel;
        }

        private PartialResult UpdateOrderPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orders = _postgresRepository.GetOrders();
                var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                order.Notes = StringGeneratorUtil.GenerateRandomString();
                stopwatch.Start();
                _postgresRepository.UpdateOrder(order);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult UpdateOrderMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orders = _msRepository.GetOrders();
                var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                order.Notes = StringGeneratorUtil.GenerateRandomString();
                stopwatch.Start();
                _msRepository.UpdateOrder(order);
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        public ResultViewModel UpdateOrderAndItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(UpdateOrderAndItems),
                PostgreSQL = UpdateOrderAndItemsPostgres(),
                MS_SQL = UpdateOrderAndItemsMS()
            };
            return resultViewModel;
        }

        private PartialResult UpdateOrderAndItemsPostgres()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orders = _postgresRepository.GetOrdersWithItems();
                var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                order.Notes = StringGeneratorUtil.GenerateRandomString();
                var orderItems = order.OrderItems;
                foreach (var item in orderItems)
                {
                    item.Note = StringGeneratorUtil.GenerateRandomString();
                }

                stopwatch.Start();
                _postgresRepository.UpdateOrder(order);
                foreach (var item in orderItems)
                {
                    _postgresRepository.UpdateOrderItem(item);
                }
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }

        private PartialResult UpdateOrderAndItemsMS()
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                var orders = _msRepository.GetOrdersWithItems();
                var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                order.Notes = StringGeneratorUtil.GenerateRandomString();
                var orderItems = order.OrderItems;
                foreach (var item in orderItems)
                {
                    item.Note = StringGeneratorUtil.GenerateRandomString();
                }

                stopwatch.Start();
                _msRepository.UpdateOrder(order);
                foreach (var item in orderItems)
                {
                    _msRepository.UpdateOrderItem(item);
                }
                stopwatch.Stop();
                results.Add((double)stopwatch.ElapsedMilliseconds);
            }

            return PartialResultUtil.CreatePartialResult(results);
        }
    }
}
