using Microsoft.AspNetCore.Mvc;
using SportStore.Model;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SqlCustomersRepository : ICustomerRepository
    {
        private readonly SportStoreContext _context;

        public SqlCustomersRepository(SportStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customers GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(p => p.Id == id);
        }

        public Customers Login(LoginViewModel customer)
        {
            return _context.Customers.SingleOrDefault(m => m.Firstname == customer.Firstname && m.Password == customer.Password);
        }

        public void Register(Customers customer)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (customer == null)
                    {
                        throw new ArgumentNullException(nameof(customer));
                    }
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
