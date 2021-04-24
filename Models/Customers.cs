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

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<CreditCarts> CreditCarts { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
