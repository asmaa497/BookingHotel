using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50),Required]
        public string Name { get; set; }
        [MaxLength(50),Required]
        public string Location { get; set; }
        [MaxLength(50),Required]
        public string City { get; set; }
        public virtual ICollection<Room>? Rooms { get; set; } 
    }
}
