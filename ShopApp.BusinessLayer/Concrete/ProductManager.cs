using ShopApp.BusinessLayer.Abstract;
using ShopApp.DataAccessLayer.Abstract;
using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.BusinessLayer.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDAL _productDAL;

        public ProductManager(IProductDAL productDAL)
        {
            _productDAL = productDAL;
        }

        public Product GetByIDWithCategories(int id)
        {
            return _productDAL.GetByIDWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _productDAL.GetCountByCategory(category);
        }

        public Product GetProductDetails(int id)
        {
            return _productDAL.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            return _productDAL.GetProductsByCategory(category, page, pageSize);
        }

        public void TAdd(Product entity)
        {
            _productDAL.Create(entity);
        }

        public void TDelete(Product entity)
        {
            _productDAL.Delete(entity);
        }

        public Product TGetByID(int id)
        {
            return _productDAL.GetByID(id);
        }

        public List<Product> TGetList()
        {
            return _productDAL.GetList();
        }

        public void TUpdate(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productDAL.Update(entity, categoryIds);
        }
    }
}
