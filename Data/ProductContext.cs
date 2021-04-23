using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> opt) : base(opt)
        {

        }

        public DbSet<Product> Products { get; set; }

    }
}
