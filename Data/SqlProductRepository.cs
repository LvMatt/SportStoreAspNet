using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SqlProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public SqlProductRepository(ProductContext context)
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

        public void CreateProducts(Products product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _context.Products.Add(product);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}
