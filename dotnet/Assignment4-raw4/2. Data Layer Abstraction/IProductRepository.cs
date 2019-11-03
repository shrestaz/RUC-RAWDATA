using _0._Models;
using _2._Data_Layer_Abstraction.Generic_Repository_Interfaces;
using System.Collections.Generic;

namespace _2._Data_Layer_Abstraction
{
    public interface IProductRepository : IGetSingleRepository<Product>
    {
        IEnumerable<Product> GetByContainedSubstringInName(string substring);
        IEnumerable<Product> GetByCategoryId(int categoryId);
    }
}