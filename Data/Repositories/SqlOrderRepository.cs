using SportStore.Data.Interfaces;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SportStore.Data.Repositories
{
    public class SqlOrderRepository : IOrderRepository
    {
        private readonly SportStoreContext _context;

        public SqlOrderRepository(SportStoreContext context)
        {
            _context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Orders GetOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Orders> GetAllOrders()
        {
            return _context.Orders.ToList();
        }

        public void CreateOrder(Orders order)
        {
            _context.Add(order);
        }
        public void UpdateOrder(Orders neworder)
        {
            if (neworder == null)
            {
                throw new ArgumentNullException(nameof(neworder));
            }

            var oldorder = _context.Orders.FirstOrDefault(p => p.Id == neworder.Id);
            if (oldorder != null)
            {
                oldorder.Id = neworder.Id;
                oldorder.Amount = neworder.Amount;
                oldorder.PaymentId = neworder.PaymentId;
                oldorder.CartId = neworder.CartId;
                oldorder.BranchesId = neworder.BranchesId;
                oldorder.CustomersId = neworder.CustomersId;
            }
        }

        public IEnumerable<Orderdetails> GetOrderDetails(int id)
        {
            var list = _context.Orderdetails.ToList();
            return list.FindAll(i => i.OrdersId == id);
        }

        public void DeleteOrder(int id)
        {
            var deletedorder = _context.Orders.FirstOrDefault(p => p.Id == id);
            _context.Orders.Remove(deletedorder);
            
            foreach(var detail in _context.Orderdetails)
            {
                if(detail.OrdersId == id)
                {
                    _context.Orderdetails.Remove(detail);
                }
            }
            
            /*var oldlist = _context.Orderdetails.ToList();
            oldlist.RemoveAll(x => x.OrdersId == id);
            _context.Orderdetails = oldlist;*/
        }

       
    }
}
