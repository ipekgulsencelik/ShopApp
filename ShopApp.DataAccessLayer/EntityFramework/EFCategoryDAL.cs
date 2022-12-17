using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccessLayer.Abstract;
using ShopApp.DataAccessLayer.Concrete;
using ShopApp.DataAccessLayer.Repository;
using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccessLayer.EntityFramework
{
    public class EFCategoryDAL : GenericRepository<Category, ShopContext>, ICategoryDAL
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from ProductCategory where ProductId=@p0 And CategoryId=@p1";

                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }

        public Category GetByIDWithProducts(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                        .Where(x => x.ID == id)
                        .Include(x => x.ProductCategories)
                        .ThenInclude(x => x.Product)
                        .FirstOrDefault();
            }
        }
    }
}
