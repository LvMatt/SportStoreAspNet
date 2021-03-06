using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Productratings
    {
        public int Id { get; set; }
        public int? Ratings { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ProductsId { get; set; }
        public int CustomersId { get; set; }

        public virtual Customers Customers { get; set; }
        public virtual Products Products { get; set; }
    }
}
