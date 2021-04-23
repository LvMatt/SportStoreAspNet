using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class ProductCategories
    {
        public ProductCategories()
        {
            ProductsCategories = new HashSet<ProductsCategories>();
        }

        public int ProductCategoriesId { get; set; }
        public string ProductCategoriesType { get; set; }
        public string ProductCategoriesDescription { get; set; }

        public virtual ICollection<ProductsCategories> ProductsCategories { get; set; }
    }
}
