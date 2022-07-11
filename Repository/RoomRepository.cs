using BookingHotel.DTO;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repository
{
    public class RoomRepository : IRepositoryRoom
    {
        private readonly ApplicationContext db;

        public RoomRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public Room Add(Room entity)
        {
            if (entity != null)
            {
                db.Rooms.Add(entity);
                db.SaveChanges();
                return entity;
            }
            throw new Exception("Insert faild");
        }
        public int Delete(int id)
        {
            Room room = db.Rooms.Find(id);
            if (room != null)
            {
                db.Rooms.Remove(room);
                return (db.SaveChanges());
            }
            return 0;
        }

        public int Edit(int id, Room newRoom)
        {
            Room oldRoom = db.Rooms.FirstOrDefault(r => r.Id == id);
            if (oldRoom != null)
            {
                oldRoom.Price = newRoom.Price;
                oldRoom.Status = newRoom.Status;
                oldRoom.Branch_Id = newRoom.Branch_Id;
                oldRoom.RoomType_Id = newRoom.RoomType_Id;
                return (db.SaveChanges());
            }
            return 0;
        }

        public ICollection<Room> GetAll()
        {
            List<Room> rooms = db.Rooms.Include(r=>r.Branch).Include(r=>r.Room_Type).AsSplitQuery().ToList();
            return (rooms);
        }

        public ICollection<Room> GetAvialable()
        {
            List<Room> rooms = db.Rooms.Where(r=>r.Status == StatusRoom.Available).Include(r => r.Branch).Include(r => r.Room_Type).AsSplitQuery().ToList();
            return rooms;
        }

        public Room GetOne(int id)
        {
            Room room = db.Rooms.Include(r=>r.Room_Type).FirstOrDefault(b => b.Id == id);
            return room;
        }

        public ICollection<Room> GetRoomsByBranchId(int branchId)
        {
            var data = db.Rooms.Where(r => r.Branch_Id == branchId).Include(r=>r.Branch).Include(r=>r.Room_Type).AsSplitQuery().ToList();
            if(data != null)
            {
                return data;
            }
            throw new Exception("Branch Empty");
        }
    }
}
