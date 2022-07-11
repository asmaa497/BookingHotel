using BookingHotel.Models;

namespace BookingHotel.Repository
{
    public class ReservationRoomRepository : IRepositoryReservationRoom
    {
        private readonly ApplicationContext db;

        public ReservationRoomRepository(ApplicationContext _db)
        {
            this.db = _db;
        }
        public int Add(ReservationRoom reservationRoom)
        {
            if (reservationRoom != null)
            {
                db.ReservationRooms.Add(reservationRoom);
                return (db.SaveChanges());
            }
            return 0;
        }
    }
}
