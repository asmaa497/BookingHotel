using BookingHotel.DTO;
using BookingHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingHotel.Repository
{
    public class ReservationRepository : IRepositoryReservation
    {
        private readonly ApplicationContext db;
        private readonly IRepositoryRoom repositoryRoom;

        public ReservationRepository(ApplicationContext _db, IRepositoryRoom _repositoryRoom)
        {
            this.db = _db;
            this.repositoryRoom = _repositoryRoom;
        }

        public Reservation Add(Reservation entity)
        {   
            if (entity != null)
            {
                db.Reservations.Add(entity);
                db.SaveChanges();
                return entity;
            }
            throw new Exception("Insert faild");
        }

        public bool CancleReservation(int reservationId,int roomId)
        {
            ReservationRoom reservationRoom;
            var data = db.Reservations.Include(r => r.Guest).Include(r => r.ReservationRooms).AsSplitQuery().FirstOrDefault(r => r.Id == reservationId);
            if(data != null)
            {
                foreach (var item in data.ReservationRooms)
                {
                    if(item.Room_Id == roomId && item.Reservation_Id == reservationId)
                    {
                        var result = repositoryRoom.GetOne(roomId);
                        result.Status = StatusRoom.Available;
                        reservationRoom = data.ReservationRooms.FirstOrDefault(r=>r.Reservation_Id == reservationId && r.Room_Id == roomId);
                        data.ReservationRooms.Remove(reservationRoom);
                        if (data.ReservationRooms.Count == 0)
                        {
                            db.Reservations.Remove(data);
                        }
                        data.TotalPrice -= item.TotalPriceForOneRoom;
                        db.SaveChanges();
                        return true;
                    }
                }

            }
            return false;
        }

        public bool CancleReservationForAllRooms(int reservationId)
        {
            var data = db.Reservations.FirstOrDefault(r => r.Id == reservationId);
            if(data != null)
            {
                db.Reservations.Remove(data);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public StatusResponse ConfirmReservation(int reservationId)
        {
            var data = db.Reservations.Include(r=>r.Guest).Include(r=>r.ReservationRooms).AsSplitQuery().FirstOrDefault(r => r.Id == reservationId);
            if (data != null)
            {
                data.Status = true;
                foreach (var item in data.ReservationRooms)
                {
                    if (item.DateIn.Date == DateTime.Now.Date)
                    {
                        var result = repositoryRoom.GetOne(item.Room_Id);
                        result.Status = StatusRoom.Booked;
                    }
                }
                db.SaveChanges();
                return new StatusResponse { Message = "Booking Done", Status = true };
            }
            return new StatusResponse { Message = $"No reservation found by this id {reservationId} ", Status = false };
        }

        public int Delete(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            if (reservation != null)
            {
                db.Reservations.Remove(reservation);
                return (db.SaveChanges());
            }
            return 0;
        }

        public int Edit(int id, Reservation newReservation)
        {
            Reservation oldReservation = db.Reservations.FirstOrDefault(r => r.Id == id);
            if (oldReservation != null)
            {
                oldReservation.Date = newReservation.Date;
                oldReservation.Status = newReservation.Status;
                oldReservation.Guest_Id = newReservation.Guest_Id;
                return (db.SaveChanges());
            }
            return 0;
        }

        public ICollection<Reservation> GetAll()
        {
            List<Reservation> reservation = db.Reservations.ToList();
            return (reservation);
        }
        public Reservation GetOne(int id)
        {
            Reservation reservation = db.Reservations.FirstOrDefault(b => b.Id == id);
            return reservation;
        }

        public Reservation GetReservationByGuestId(string guestId)
        {
            var data = db.Reservations.FirstOrDefault(r => r.Guest_Id == guestId);
            if(data != null)
            {
                return (data);
            }
            throw new Exception("No found reservation for this guest");
        }

        public List<Reservation> GetReservationsForGuest(string guestId)
        {
            var data = db.Reservations.Where(r => r.Guest_Id == guestId).Include(r=>r.ReservationRooms).ThenInclude(r=>r.Room).AsSplitQuery().ToList();
            if(data !=null)
            {
                return (data);
            }
            throw new Exception("No found reservation for this guest");
        }
    }
}
