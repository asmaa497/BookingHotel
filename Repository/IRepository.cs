using BookingHotel.DTO;

namespace BookingHotel.Repository
{
    public interface IRepository<T1,T2> where T1:class
    {
        ICollection<T1> GetAll();
        T1 Add(T1 entity);
        int Delete(T2 id);
        T1 GetOne(T2 id);
        int Edit(T2 id,T1 entity);
    }
}
