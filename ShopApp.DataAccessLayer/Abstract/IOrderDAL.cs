using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccessLayer.Abstract
{
    public interface IOrderDAL : IGenericDAL<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
