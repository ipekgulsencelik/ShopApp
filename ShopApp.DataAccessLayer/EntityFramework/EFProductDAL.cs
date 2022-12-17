using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccessLayer.Abstract;
using ShopApp.DataAccessLayer.Concrete;
using ShopApp.DataAccessLayer.Repository;
using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccessLayer.EntityFramework
{
    public class EFProductDAL : GenericRepository<Product, ShopContext>, IProductDAL
    {
        public Product GetByIDWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                        .Where(x => x.ID == id)
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Category)
                        .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(x => x.ProductCategories)
                                .ThenInclude(x => x.Category)
                                .Where(x => x.ProductCategories.Any(c => c.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Count();
            }
        }

        public Product GetProductDetails(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Products
                            .Where(x => x.ID == id)
                            .Include(x => x.ProductCategories)
                            .ThenInclude(x => x.Category)
                            .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                                .Include(x => x.ProductCategories)
                                .ThenInclude(x => x.Category)
                                .Where(x => x.ProductCategories.Any(c => c.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Product model, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Products
                                   .Include(x => x.ProductCategories)
                                   .FirstOrDefault(x => x.ID == model.ID);

                if (product != null)
                {
                    product.Name = model.Name;
                    product.Description = model.Description;
                    product.ImageURL = model.ImageURL;
                    product.Price = model.Price;

                    product.ProductCategories = categoryIds.Select(categoryId => new ProductCategory()
                    {
                        CategoryID = categoryId,
                        ProductID = model.ID
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}