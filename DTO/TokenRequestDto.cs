using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class TokenRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
