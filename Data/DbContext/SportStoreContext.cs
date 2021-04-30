﻿using Microsoft.EntityFrameworkCore;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public class SportStoreContext : DbContext
    {
        public SportStoreContext(DbContextOptions<SportStoreContext> opt) : base(opt)
        {

        }

        public SportStoreContext() : base()
        {

        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Orderdetails> Orderdetails { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<Productratings> Productratings { get; set; }


    }
}