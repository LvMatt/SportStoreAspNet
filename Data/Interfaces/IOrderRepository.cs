using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data.Interfaces
{
    public interface IOrderRepository
    {
        bool SaveChanges();
        IEnumerable<Orders> GetAllOrders();

        Orders GetOrderById(int id);
        void CreateOrder(Orders order);
        void UpdateOrder(Orders order);
        void DeleteOrder(int id);
        IEnumerable<Orderdetails> GetOrderDetails(int id);

    }
}
