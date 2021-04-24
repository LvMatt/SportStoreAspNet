using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Models
{
    public partial class ProductsOrders
    {
        [Key]
        public int ProductsId { get; set; }
        public int OrdersId { get; set; }

        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
    }
}
