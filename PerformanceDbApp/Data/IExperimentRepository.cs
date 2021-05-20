using PerformanceDbApp.Models;
using PerformanceDbApp.ViewModels;

namespace PerformanceDbApp.Data
{
    public interface IExperimentRepository
    {
        ResultViewModel SelectAllOrders();
        ResultViewModel SelectAllOrdersWithItems();
        ResultViewModel SelectAllOrdersWithItemsAndProducts();
    }
}