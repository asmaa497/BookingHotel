using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingHotel.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.Date), Required]
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        [ForeignKey("Guest"), Required]
        public string Guest_Id { get; set; }
        public double TotalPrice { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual ICollection<ReservationRoom> ReservationRooms { get; set; } 
    }
}
