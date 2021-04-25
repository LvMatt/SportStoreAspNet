using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class MockProductRepository
    {
    
        public Products GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Products> GetAllProducts()
        {
            throw new NotImplementedException();

        }

      
    }
}
