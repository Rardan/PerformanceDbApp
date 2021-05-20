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
    }
}
