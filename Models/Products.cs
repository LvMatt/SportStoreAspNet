using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Products
    {
        public Products()
        {
            ProductsCategories = new HashSet<ProductsCategories>();
            ProductsOrders = new HashSet<ProductsOrders>();
        }

        public int ProductsId { get; set; }
        public int SuppliersId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public byte[] ProductImageData { get; set; }
        public decimal? ProductWeight { get; set; }
        public string ProductImageName { get; set; }

        public virtual Suppliers Suppliers { get; set; }
        public virtual ICollection<ProductsCategories> ProductsCategories { get; set; }
        public virtual ICollection<ProductsOrders> ProductsOrders { get; set; }
    }
}
