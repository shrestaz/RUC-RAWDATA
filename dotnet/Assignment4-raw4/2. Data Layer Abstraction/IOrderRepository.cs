using _0._Models;
using _2._Data_Layer_Abstraction.Generic_Repository_Interfaces;
using System.Collections.Generic;

namespace _2._Data_Layer_Abstraction
{
    public interface IOrderRepository : IGetSingleRepository<Order>, IGetAllRepository<Order>
    {
        IEnumerable<Order> GetByShippingName(string shippingName);
    }
}