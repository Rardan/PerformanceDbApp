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
            var results = new List<ResultViewModel>();

            results.Add(_experimentRepository.SelectAllOrders());
            results.Add(_experimentRepository.SelectAllOrdersWithItems());
            results.Add(_experimentRepository.SelectAllOrdersWithItemsAndProducts());

            return View(results);
        }
    }
}
