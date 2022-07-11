using System.ComponentModel.DataAnnotations;

namespace BookingHotel.Models
{
    [Flags]
    public enum TypeOfRoom
    {
        Single,
        Double,
        Suite 
    }
    public class RoomType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public TypeOfRoom TypeRoom { get; set; }
        [Required]
        public int Capacity { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}