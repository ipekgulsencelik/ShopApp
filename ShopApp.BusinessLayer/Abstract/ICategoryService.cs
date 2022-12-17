using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.BusinessLayer.Abstract
{
    public interface ICategoryService : IGenericService<Category>
    {
        Category GetByIDWithProducts(int id);
        void DeleteFromCategory(int categoryId, int productId);
    }
}
