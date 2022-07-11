using BookingHotel.DTO;
using BookingHotel.Models;

namespace BookingHotel.Repository
{
    public class RoomTypeRepository : IRepositoryRoomType
    {
        private readonly ApplicationContext db;

        public RoomTypeRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public RoomType Add(RoomType entity)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public int Edit(int id, RoomType entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<RoomType> GetAll()
        {
            throw new NotImplementedException();
        }

        public RoomType GetOne(int id)
        {
            throw new NotImplementedException();
        }
    }
}
