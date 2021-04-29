using SportStore.Models;
using SportStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Data
{
    public interface IProductRepository
    {
        bool SaveChanges();
        IEnumerable<Products> GetAllProducts();
        IEnumerable<Productratings> GetAllProductRatings();
        public IQueryable<string> GetAllProductRatingsByProductId(int id);


        Products GetProductById(int id);
        void CreateProducts(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(int id);
        void CreateProductReview(Productratings ratings, int userId, int productId);
    }
}
