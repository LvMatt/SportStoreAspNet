﻿using SportStore.Model;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SqlProductRepository : ICustomerRepository, IProductRepository
    {
        private readonly SportStoreContext _context;

        public SqlProductRepository(SportStoreContext context)
        {
            _context = context;
        }


        public Products GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Products> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Customers> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customers GetCustomerById(int id)
        {
            return _context.Customers.FirstOrDefault(p => p.Id == id);
        }

        public void CreateProducts(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
        }

        public void UpdateProduct(Products newproduct)
        {
            if (newproduct == null)
            {
                throw new ArgumentNullException(nameof(newproduct));
            }

            var oldproduct = _context.Products.FirstOrDefault(p => p.Id == newproduct.Id);
            if (oldproduct != null)
            {
                oldproduct.Id = newproduct.Id;
                oldproduct.SuppliersId = newproduct.SuppliersId;
                oldproduct.Name = newproduct.Name;
                oldproduct.Price = newproduct.Price;
                oldproduct.Description = newproduct.Description;
                oldproduct.ImageData = newproduct.ImageData;
                oldproduct.Weight = newproduct.Weight;
                oldproduct.ImageName = newproduct.ImageName;
            }
        }

        public void DeleteProduct(int id)
        {
            var deletedproduct = _context.Products.FirstOrDefault(p => p.Id == id);
            _context.Products.Remove(deletedproduct);
        }

        public IEnumerable<Customers> CustomerDetails()
        {
            var customers =_context.Customers.ToList();
            return customers;
        }

        public Customers Login(LoginViewModel customer)
        {
              return _context.Customers.SingleOrDefault(m => m.Firstname == customer.Firstname && m.Password == customer.Password);
        }

        public void Register(Customers customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            _context.Customers.Add(customer);
        }


        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
