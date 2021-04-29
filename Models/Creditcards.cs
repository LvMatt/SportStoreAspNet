using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Creditcards
    {
        public int Id { get; set; }
        public string Validation { get; set; }
        public DateTime Expiration { get; set; }
        public string Extradetails { get; set; }
        public int CustomersId { get; set; }

        public virtual Customers Customers { get; set; }
    }
}
