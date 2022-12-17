using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccessLayer.Abstract
{
    public interface ICategoryDAL : IGenericDAL<Category>
    {
        Category GetByIDWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
