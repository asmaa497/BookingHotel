using BookingHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class ReservationDto
    {
        public List<ReservationRoomDto> ReservationRoomInfo { get; set; } = new List<ReservationRoomDto>();
        public string Guest_Id { get; set; }
    }
}
