using BookingHotel.DTO;
using BookingHotel.Models;

namespace BookingHotel.Repository
{
    public class TempGuestRoomRepository : IRepositoryTempGuestRoom
    {
        private readonly ApplicationContext db;

        public TempGuestRoomRepository(ApplicationContext _db)
        {
            db = _db;
        }
        public TempGuestRooms Add(TempGuestRooms entity)
        {
            if(entity != null)
            {
                db.TempGuestRooms.Add(entity);
                db.SaveChanges();
                return entity;
            }
            throw new Exception("Add faild");
        }

        public int Delete(int id)
        {
            var data = db.TempGuestRooms.FirstOrDefault(t => t.Id == id);
            if(data != null)
            {
                db.TempGuestRooms.Remove(data);
                return (db.SaveChanges());
            }
            return 0;
        }
        public int DeleteByGuestID(string id)
        {
            var data = db.TempGuestRooms.Where(t => t.GuestId==id).ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    db.TempGuestRooms.Remove(item);
                }
                
                return (db.SaveChanges());
            }
            return 0;
        }

        public int EditTempRoom(int id, TempRoomDTO entity)
        {
            TempGuestRooms tempGuestRooms = db.TempGuestRooms.FirstOrDefault(t => t.Id == id); 
            if(tempGuestRooms != null)
            {
                tempGuestRooms.NumberOfDays = entity.DateOut.Day-entity.DateIn.Day;

                tempGuestRooms.DateIn = entity.DateIn;
                tempGuestRooms.DateOut = entity.DateOut;
                return (db.SaveChanges());
            }
            return (0);
        }

        public ICollection<TempGuestRooms> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TempGuestRooms> GetAllForGuest(string guestId)
        {
            var data = db.TempGuestRooms.Where(t => t.GuestId == guestId).ToList();
            return (data);
        }
        public TempGuestRooms GetOne(int id)
        {
            var data = db.TempGuestRooms.FirstOrDefault(t => t.Id == id);
            return data;
        }

        public bool CheckIfTempRoomExit(int roomId, string guestId)
        {
            var data = db.TempGuestRooms.FirstOrDefault(t => t.RoomId == roomId && t.GuestId == guestId);
            if(data != null)
            {
                return true;
            }
            return false;

        }

        public int Edit(int id, TempGuestRooms entity)
        {
            throw new NotImplementedException();
        }
    }
}
