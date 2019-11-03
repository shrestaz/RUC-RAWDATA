using System.Collections.Generic;

namespace _2._Data_Layer_Abstraction.Generic_Repository_Interfaces
{
    public interface IGetAllRepository<T>
    {
        IEnumerable<T> GetAll();
    }
}