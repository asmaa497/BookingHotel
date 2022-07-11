using BookingHotel.Models;
using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class BranchDto
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        [MaxLength(50), Required]
        public string Location { get; set; }
        [MaxLength(50), Required]
        public string City { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
