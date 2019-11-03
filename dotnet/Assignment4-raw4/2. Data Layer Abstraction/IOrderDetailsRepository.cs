using _0._Models;
using System.Collections.Generic;

namespace _2._Data_Layer_Abstraction
{
    public interface IOrderDetailsRepository
    {
        IEnumerable<OrderDetails> GetByOrderId(int id);
        IEnumerable<OrderDetails> GetByProductId(int id);
    }
}