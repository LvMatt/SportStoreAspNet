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
        private readonly mydbContext _context;

        public SqlOrderRepository(mydbContext context)
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
            _context.SaveChanges();
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
            _context.SaveChanges();
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

            _context.SaveChanges();
            /*var oldlist = _context.Orderdetails.ToList();
            oldlist.RemoveAll(x => x.OrdersId == id);
            _context.Orderdetails = oldlist;*/
        }
        
        enum Result {Added, InvalidProduct, InvalidOrder }

        public int AddToOrder(Orderdetails orderdetails)
        {
            _context.Set<SportStoreContext>().FromSqlRaw("cart_validation");
            var product = _context.Products.FirstOrDefault(x => x.Id == orderdetails.ProductsId);
            if (product == null) return (int)Result.InvalidProduct;
            _context.Orderdetails.Add(orderdetails);
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderdetails.OrdersId);

            if(order != null)
            {
                order.Amount = order.Amount + Convert.ToDecimal(orderdetails.Price);
                UpdateOrder(order);
                _context.SaveChanges();
            }
            else
            {
                return (int)Result.InvalidOrder;
            }
            _context.SaveChanges();
            return (int) Result.Added;
        }
    }
}
