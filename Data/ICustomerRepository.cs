using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public interface ICustomerRepository
    {
        public void Register(Customers customer);
        public Customers Login(LoginViewModel customer);
        public IEnumerable<Customers> GetAllCustomers();
    }
}
