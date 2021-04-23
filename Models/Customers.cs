using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Customers
    {
        public Customers()
        {
            CreditCarts = new HashSet<CreditCarts>();
            Orders = new HashSet<Orders>();
        }

        public int CustomerId { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }

        public virtual ICollection<CreditCarts> CreditCarts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
