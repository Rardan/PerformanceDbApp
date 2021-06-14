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
                var order1 = PrepareOrder();

                var order2 = PrepareOrder();

                var product1 = PrepareProduct();

                var product2 = PrepareProduct();

                var product3 = PrepareProduct();

                var product4 = PrepareProduct();

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
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    SelectAllOrdersPostgres(1),
                    SelectAllOrdersPostgres(10),
                    SelectAllOrdersPostgres(20),
                    SelectAllOrdersPostgres(50),
                    SelectAllOrdersPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    SelectAllOrdersMS(1),
                    SelectAllOrdersMS(10),
                    SelectAllOrdersMS(20),
                    SelectAllOrdersMS(50),
                    SelectAllOrdersMS(100))
            };
            return resultViewModel;
        }

        private TimeResult SelectAllOrdersPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _postgresRepository.GetOrders();
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult SelectAllOrdersMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.GetOrders();
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel SelectAllOrdersWithItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectAllOrdersWithItems),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    SelectAllOrdersWithItemsPostgres(1),
                    SelectAllOrdersWithItemsPostgres(10),
                    SelectAllOrdersWithItemsPostgres(20),
                    SelectAllOrdersWithItemsPostgres(50),
                    SelectAllOrdersWithItemsPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    SelectAllOrdersWithItemsMS(1),
                    SelectAllOrdersWithItemsMS(10),
                    SelectAllOrdersWithItemsMS(20),
                    SelectAllOrdersWithItemsMS(50),
                    SelectAllOrdersWithItemsMS(100))
            };
            return resultViewModel;
        }

        private TimeResult SelectAllOrdersWithItemsPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _postgresRepository.GetOrdersWithItems();
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult SelectAllOrdersWithItemsMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.GetOrdersWithItems();
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel SelectAllOrdersWithItemsAndProducts()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectAllOrdersWithItemsAndProducts),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    SelectAllOrdersWithItemsAndProductsPostgres(1),
                    SelectAllOrdersWithItemsAndProductsPostgres(10),
                    SelectAllOrdersWithItemsAndProductsPostgres(20),
                    SelectAllOrdersWithItemsAndProductsPostgres(50),
                    SelectAllOrdersWithItemsAndProductsPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    SelectAllOrdersWithItemsAndProductsMS(1),
                    SelectAllOrdersWithItemsAndProductsMS(10),
                    SelectAllOrdersWithItemsAndProductsMS(20),
                    SelectAllOrdersWithItemsAndProductsMS(50),
                    SelectAllOrdersWithItemsAndProductsMS(100))
            };
            return resultViewModel;
        }

        private TimeResult SelectAllOrdersWithItemsAndProductsPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _postgresRepository.GetOrdersWithItems();
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult SelectAllOrdersWithItemsAndProductsMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.GetOrdersWithItems();
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel SelectOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectOrder),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    SelectOrderPostgres(1),
                    SelectOrderPostgres(10),
                    SelectOrderPostgres(20),
                    SelectOrderPostgres(50),
                    SelectOrderPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    SelectOrderMS(1),
                    SelectOrderMS(10),
                    SelectOrderMS(20),
                    SelectOrderMS(50),
                    SelectOrderMS(100))
            };
            return resultViewModel;
        }

        private TimeResult SelectOrderPostgres(int times)
        {
            List<double> results = new List<double>();

            var ids = _postgresRepository.GetOrdersIds();
            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var orders = _postgresRepository.GetOrderById(id);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult SelectOrderMS(int times)
        {
            List<double> results = new List<double>();

            var ids = _msRepository.GetOrdersIds();
            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.GetOrderById(id);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel SelectOrderWithItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectOrderWithItems),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    SelectOrderWithItemsPostgres(1),
                    SelectOrderWithItemsPostgres(10),
                    SelectOrderWithItemsPostgres(20),
                    SelectOrderWithItemsPostgres(50),
                    SelectOrderWithItemsPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    SelectOrderWithItemsMS(1),
                    SelectOrderWithItemsMS(10),
                    SelectOrderWithItemsMS(20),
                    SelectOrderWithItemsMS(50),
                    SelectOrderWithItemsMS(100))
            };
            return resultViewModel;
        }

        private TimeResult SelectOrderWithItemsPostgres(int times)
        {
            List<double> results = new List<double>();

            var ids = _postgresRepository.GetOrdersIds();
            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var orders = _postgresRepository.GetOrderByIdIncludeOrderItems(id);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult SelectOrderWithItemsMS(int times)
        {
            List<double> results = new List<double>();

            var ids = _msRepository.GetOrdersIds();
            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.GetOrderByIdIncludeOrderItems(id);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }
            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel SelectOrderWithItemsAndProducts()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(SelectOrderWithItemsAndProducts),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    SelectOrderWithItemsAndProductsPostgres(1),
                    SelectOrderWithItemsAndProductsPostgres(10),
                    SelectOrderWithItemsAndProductsPostgres(20),
                    SelectOrderWithItemsAndProductsPostgres(50),
                    SelectOrderWithItemsAndProductsPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    SelectOrderWithItemsAndProductsMS(1),
                    SelectOrderWithItemsAndProductsMS(10),
                    SelectOrderWithItemsAndProductsMS(20),
                    SelectOrderWithItemsAndProductsMS(50),
                    SelectOrderWithItemsAndProductsMS(100))
            };
            return resultViewModel;
        }

        private TimeResult SelectOrderWithItemsAndProductsPostgres(int times)
        {
            List<double> results = new List<double>();

            var ids = _postgresRepository.GetOrdersIds();
            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var orders = _postgresRepository.GetOrderByIdIncludeOrderItemsAndProducts(id);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult SelectOrderWithItemsAndProductsMS(int times)
        {
            List<double> results = new List<double>();

            var ids = _msRepository.GetOrdersIds();
            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var id = ids.FirstOrDefault(i => i == RandomNumberUtil.GenerateFromRange(0, ids.Count()));
                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.GetOrderByIdIncludeOrderItemsAndProducts(id);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        // create
        public ResultViewModel CreateOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(CreateOrder),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    CreateOrderPostgres(1),
                    CreateOrderPostgres(10),
                    CreateOrderPostgres(20),
                    CreateOrderPostgres(50),
                    CreateOrderPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    CreateOrderMS(1),
                    CreateOrderMS(10),
                    CreateOrderMS(20),
                    CreateOrderMS(50),
                    CreateOrderMS(100))
            };
            return resultViewModel;
        }

        private TimeResult CreateOrderPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var newOrder = PrepareOrder();
                    stopwatch = Stopwatch.StartNew();
                    var order = _postgresRepository.CreateOrder(newOrder);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult CreateOrderMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var newOrder = PrepareOrder();
                    stopwatch = Stopwatch.StartNew();
                    var order = _msRepository.CreateOrder(newOrder);
                    stopwatch.Stop();
                    results.Add((double)stopwatch.ElapsedMilliseconds);
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel CreateOrderAndItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(CreateOrderAndItems),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    CreateOrderAndItemsPostgres(1),
                    CreateOrderAndItemsPostgres(10),
                    CreateOrderAndItemsPostgres(20),
                    CreateOrderAndItemsPostgres(50),
                    CreateOrderAndItemsPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    CreateOrderAndItemsMS(1),
                    CreateOrderAndItemsMS(10),
                    CreateOrderAndItemsMS(20),
                    CreateOrderAndItemsMS(50),
                    CreateOrderAndItemsMS(100))
            };
            return resultViewModel;
        }

        private TimeResult CreateOrderAndItemsPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var newOrder = PrepareOrder();
                    var newProduct = PrepareProduct();
                    var product = _postgresRepository.CreateProduct(newProduct);

                    stopwatch = Stopwatch.StartNew();
                    var order = _postgresRepository.CreateOrderWithItems(newOrder, new List<Product>() { product });
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult CreateOrderAndItemsMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var newOrder = PrepareOrder();
                    var newProduct = PrepareProduct();
                    var product = _msRepository.CreateProduct(newProduct);

                    stopwatch = Stopwatch.StartNew();
                    var orders = _msRepository.CreateOrderWithItems(newOrder, new List<Product>() { product });
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        // delete
        public ResultViewModel DeleteOrderItem()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(DeleteOrderItem),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    DeleteOrderItemPostgres(1),
                    DeleteOrderItemPostgres(10),
                    DeleteOrderItemPostgres(20),
                    DeleteOrderItemPostgres(50),
                    DeleteOrderItemPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    DeleteOrderItemMS(1),
                    DeleteOrderItemMS(10),
                    DeleteOrderItemMS(20),
                    DeleteOrderItemMS(50),
                    DeleteOrderItemMS(100))
            };
            return resultViewModel;
        }

        private TimeResult DeleteOrderItemPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orderItems = _postgresRepository.GetOrderItems();
                    var orderItem = orderItems.ElementAt(RandomNumberUtil.GenerateFromRange(0, orderItems.Count()));
                    stopwatch = Stopwatch.StartNew();
                    _postgresRepository.DeleteOrderItem(orderItem);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult DeleteOrderItemMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orderItems = _msRepository.GetOrderItems();
                    var orderItem = orderItems.ElementAt(RandomNumberUtil.GenerateFromRange(0, orderItems.Count()));
                    stopwatch = Stopwatch.StartNew();
                    _msRepository.DeleteOrderItem(orderItem);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel DeleteOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(DeleteOrder),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    DeleteOrderPostgres(1),
                    DeleteOrderPostgres(10),
                    DeleteOrderPostgres(20),
                    DeleteOrderPostgres(50),
                    DeleteOrderPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    DeleteOrderMS(1),
                    DeleteOrderMS(10),
                    DeleteOrderMS(20),
                    DeleteOrderMS(50),
                    DeleteOrderMS(100))
            };
            return resultViewModel;
        }

        private TimeResult DeleteOrderPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _postgresRepository.GetOrders();
                    var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    stopwatch = Stopwatch.StartNew();
                    _postgresRepository.DeleteOrder(order);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult DeleteOrderMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _msRepository.GetOrders();
                    var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    stopwatch = Stopwatch.StartNew();
                    _msRepository.DeleteOrder(order);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        // update
        public ResultViewModel UpdateOrder()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(UpdateOrder),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    UpdateOrderPostgres(1),
                    UpdateOrderPostgres(10),
                    UpdateOrderPostgres(20),
                    UpdateOrderPostgres(50),
                    UpdateOrderPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    UpdateOrderMS(1),
                    UpdateOrderMS(10),
                    UpdateOrderMS(20),
                    UpdateOrderMS(50),
                    UpdateOrderMS(100))
            };
            return resultViewModel;
        }

        private TimeResult UpdateOrderPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _postgresRepository.GetOrders();
                    var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    order.Notes = StringGeneratorUtil.GenerateRandomString();
                    stopwatch = Stopwatch.StartNew();
                    _postgresRepository.UpdateOrder(order);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult UpdateOrderMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _msRepository.GetOrders();
                    var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    order.Notes = StringGeneratorUtil.GenerateRandomString();
                    stopwatch = Stopwatch.StartNew();
                    _msRepository.UpdateOrder(order);
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        public ResultViewModel UpdateOrderAndItems()
        {
            ResultViewModel resultViewModel = new ResultViewModel()
            {
                Name = nameof(UpdateOrderAndItems),
                PostgreSQL = PartialResultUtil.CreatePartialResult(
                    UpdateOrderAndItemsPostgres(1),
                    UpdateOrderAndItemsPostgres(10),
                    UpdateOrderAndItemsPostgres(20),
                    UpdateOrderAndItemsPostgres(50),
                    UpdateOrderAndItemsPostgres(100)),
                MS_SQL = PartialResultUtil.CreatePartialResult(
                    UpdateOrderAndItemsMS(1),
                    UpdateOrderAndItemsMS(10),
                    UpdateOrderAndItemsMS(20),
                    UpdateOrderAndItemsMS(50),
                    UpdateOrderAndItemsMS(100))
            };
            return resultViewModel;
        }

        private TimeResult UpdateOrderAndItemsPostgres(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _postgresRepository.GetOrdersWithItems();
                    var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    order.Notes = StringGeneratorUtil.GenerateRandomString();
                    var orderItems = order.OrderItems;
                    foreach (var item in orderItems)
                    {
                        item.Note = StringGeneratorUtil.GenerateRandomString();
                    }

                    stopwatch = Stopwatch.StartNew();
                    _postgresRepository.UpdateOrder(order);
                    foreach (var item in orderItems)
                    {
                        _postgresRepository.UpdateOrderItem(item);
                    }
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        private TimeResult UpdateOrderAndItemsMS(int times)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < count; i++)
            {
                List<double> time = new List<double>();
                for (int j = 0; j < times; j++)
                {
                    var orders = _msRepository.GetOrdersWithItems();
                    var order = orders.ElementAt(RandomNumberUtil.GenerateFromRange(0, orders.Count()));
                    order.Notes = StringGeneratorUtil.GenerateRandomString();
                    var orderItems = order.OrderItems;
                    foreach (var item in orderItems)
                    {
                        item.Note = StringGeneratorUtil.GenerateRandomString();
                    }

                    stopwatch = Stopwatch.StartNew();
                    _msRepository.UpdateOrder(order);
                    foreach (var item in orderItems)
                    {
                        _msRepository.UpdateOrderItem(item);
                    }
                    stopwatch.Stop();
                    time.Add((double)stopwatch.ElapsedMilliseconds);
                }
                results.Add(time.Sum());
            }

            return PartialResultUtil.CreateTimeResult(results);
        }

        //---------------------------------------------------------//
        private Order PrepareOrder()
        {
            return new Order()
            {
                Notes = StringGeneratorUtil.GenerateRandomString(),
                OrderTotal = 20D
            };
        }

        private Product PrepareProduct()
        {
            return new Product()
            {
                Name = StringGeneratorUtil.GenerateRandomString(),
                Price = 5D,
                Type = StringGeneratorUtil.GenerateRandomString()
            };
        }
    }
}
