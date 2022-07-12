using BookingHotel.DTO;
using BookingHotel.Models;

namespace BookingHotel.Repository
{
    public interface IRepositoryTempGuestRoom:IRepository<TempGuestRooms,int>
    {
        bool CheckIfTempRoomExit(int roomId, string guestId);
        List<TempGuestRooms> GetAllForGuest(string guestId);
        int DeleteByGuestID(string id);
        int EditTempRoom(int id, TempRoomDTO tempRoomDTO);
    }
}
