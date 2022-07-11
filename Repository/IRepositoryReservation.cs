using BookingHotel.DTO;
using BookingHotel.Models;

namespace BookingHotel.Repository
{
    public interface IRepositoryReservation:IRepository<Reservation,int>
    {
        StatusResponse ConfirmReservation(int reservationId);
        bool CancleReservation(int reservationId, int roomId);
        bool CancleReservationForAllRooms(int reservationId);
        Reservation GetReservationByGuestId(string guestId);
        List<Reservation> GetReservationsForGuest(string guestId);

    }
}
