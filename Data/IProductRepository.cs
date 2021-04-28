using SportStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public interface IProductRepository
    {
        bool SaveChanges();
        IEnumerable<Products> GetAllProducts();
        Products GetProductById(int id);
        void CreateProducts(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(int id);
    }
}
