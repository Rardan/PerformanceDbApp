using PerformanceDbApp.ViewModels;

namespace PerformanceDbApp.Data
{
    public interface IExperimentRepository
    {
        void CreateDefaultData();
        ResultViewModel CreateOrder();
        ResultViewModel CreateOrderAndItems();
        void DeleteAllData();
        ResultViewModel DeleteOrder();
        ResultViewModel DeleteOrderItem();
        ResultViewModel SelectAllOrders();
        ResultViewModel SelectAllOrdersWithItems();
        ResultViewModel SelectAllOrdersWithItemsAndProducts();
        ResultViewModel SelectOrder();
        ResultViewModel SelectOrderWithItems();
        ResultViewModel SelectOrderWithItemsAndProducts();
        ResultViewModel UpdateOrder();
        ResultViewModel UpdateOrderAndItems();
    }
}