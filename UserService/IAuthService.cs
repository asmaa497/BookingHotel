using BookingHotel.DTO;
using BookingHotel.Models;

namespace BookingHotel.UserService
{
    public interface IAuthService
    {
        Task<AuthDto> Register(RegisterDto model);
        Task<AuthDto> Login(TokenRequestDto model);
        Task<string> AddRole(AddRoleDto model);
        Task<Guest> GetGuestInfo(string userName);
    }
}
