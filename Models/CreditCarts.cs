using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class CreditCarts
    {
        public CreditCarts()
        {
            Payments = new HashSet<Payments>();
        }

        public int Id { get; set; }
        public string Validation { get; set; }
        public DateTime Expiration { get; set; }
        public string Extradetails { get; set; }
        public int CustomerId { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
    }
}
