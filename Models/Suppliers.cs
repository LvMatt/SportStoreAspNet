using System;
using System.Collections.Generic;

namespace SportStore.Model
{
    public partial class Suppliers
    {
        public Suppliers()
        {
            Products = new HashSet<Products>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
