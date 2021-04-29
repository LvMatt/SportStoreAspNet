using System;
using System.Collections.Generic;

namespace SportStore.Models
{
    public partial class Products
    {
        public Products()
        {
            Orderdetails = new HashSet<Orderdetails>();
            Productratings = new HashSet<Productratings>();
        }

        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public decimal? Weight { get; set; }
        public string ImageName { get; set; }
        public int ProductCategoriesId { get; set; }
        public float? OverallRating { get; set; }

        public virtual ProductCategories ProductCategories { get; set; }
        public virtual Suppliers Suppliers { get; set; }
        public virtual ICollection<Orderdetails> Orderdetails { get; set; }
        public virtual ICollection<Productratings> Productratings { get; set; }
    }
}
