using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ShopApp.DataAccessLayer.Abstract
{
    public interface IGenericDAL<T>
    {
        T GetByID(int id);
        T GetOne(Expression<Func<T, bool>> filter);
        List<T> GetList(Expression<Func<T, bool>> filter = null);

        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}