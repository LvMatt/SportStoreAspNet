using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class ProductsOrders
    {
        public int ProductsProductsId { get; set; }
        public int OrdersOrdersId { get; set; }

        public virtual Orders OrdersOrders { get; set; }
        public virtual Products ProductsProducts { get; set; }
    }
}
