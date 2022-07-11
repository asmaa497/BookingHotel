using BookingHotel.Models;

namespace BookingHotel.Repository
{
    public interface IRepositoryRoom:IRepository<Room,int>
    {
        ICollection<Room> GetAvialable();
        ICollection<Room> GetRoomsByBranchId(int branchId);
    }
}
