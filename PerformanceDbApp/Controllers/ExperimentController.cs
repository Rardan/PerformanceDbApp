using Microsoft.AspNetCore.Mvc;
using PerformanceDbApp.Data;
using PerformanceDbApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerformanceDbApp.Controllers
{
    public class ExperimentController : Controller
    {
        private readonly IExperimentRepository _experimentRepository;

        public ExperimentController(IExperimentRepository experimentRepository)
        {
            _experimentRepository = experimentRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Results()
        {
            _experimentRepository.DeleteAllData();
            _experimentRepository.CreateDefaultData();

            var results = new List<ResultViewModel>();

            results.Add(_experimentRepository.SelectAllOrders());
            results.Add(_experimentRepository.SelectAllOrdersWithItems());
            results.Add(_experimentRepository.SelectAllOrdersWithItemsAndProducts());

            results.Add(_experimentRepository.SelectOrder());
            results.Add(_experimentRepository.SelectOrderWithItems());
            results.Add(_experimentRepository.SelectOrderWithItemsAndProducts());

            results.Add(_experimentRepository.CreateOrder());
            results.Add(_experimentRepository.CreateOrderAndItems());

            results.Add(_experimentRepository.DeleteOrderItem());
            results.Add(_experimentRepository.DeleteOrder());

            results.Add(_experimentRepository.UpdateOrder());
            results.Add(_experimentRepository.UpdateOrderAndItems());

            return View(results);
        }
    }
}
