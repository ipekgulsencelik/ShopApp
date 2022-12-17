using ShopApp.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.BusinessLayer.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        List<Order> GetOrders(string userId);
    }
}
