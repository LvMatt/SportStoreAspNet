using System;
using System.Collections.Generic;

namespace SportStore.Model
{
    public partial class Orderdetails
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public float? Price { get; set; }
        public float? Discount { get; set; }
        public int ProductsId { get; set; }
        public int OrdersId { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}
