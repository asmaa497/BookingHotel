using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models
{
    public class TempGuestRooms
    {
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateOut { get; set; }
        [ForeignKey("Guest")]
        public string GuestId { get; set; }
        public int RoomId { get; set; }
        public virtual Guest? Guest { get; set; }
    }
}
