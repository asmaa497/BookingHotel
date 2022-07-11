using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Guest:IdentityUser
    {
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<TempGuestRooms> TempGuestRooms { get; set; }
    }
}
