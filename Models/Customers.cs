using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Customers
    {
        public Customers()
        {
            Creditcards = new HashSet<Creditcards>();
            Orders = new HashSet<Orders>();
            Productratings = new HashSet<Productratings>();
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Creditcards> Creditcards { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Productratings> Productratings { get; set; }
    }
}
