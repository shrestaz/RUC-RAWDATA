namespace _2._Data_Layer_Abstraction.Generic_Repository_Interfaces
{
    public interface IGetSingleRepository<T>
    {
        T GetById(int id);
    }
}