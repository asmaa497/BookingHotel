using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models
{
    public class ReservationRoom
    {
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime DateOut { get; set; }
        [ForeignKey("Reservation")]
        public int Reservation_Id { get; set; }
        public virtual Reservation Reservation { get; set; }
        [ForeignKey("Room")]
        public int Room_Id { get; set; }
        public double TotalPriceForOneRoom { get; set; }
        public virtual Room Room { get; set; }
    }
}
