using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Options;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public interface ICustomerRepository
    {
        public  Task<AuthenticationResult> Register(Customers customer);
        public  Task<AuthenticationResult> Login(LoginViewModel customer);
        public IEnumerable<Customers> GetAllCustomers();
        public bool SaveChanges();
    }
}
