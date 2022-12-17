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
    public class EFOrderDAL : GenericRepository<Order, ShopContext>, IOrderDAL
    {
        public List<Order> GetOrders(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
