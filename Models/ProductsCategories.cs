using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportStore.Models
{
    public partial class ProductsCategories
    {
        [Key]
        public int ProductsId { get; set; }
        public int ProductCategoriesId { get; set; }

        public virtual ProductCategories ProductCategories { get; set; }
        public virtual Products Products { get; set; }
    }
}
