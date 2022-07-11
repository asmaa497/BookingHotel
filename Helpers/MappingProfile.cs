using AutoMapper;
using BookingHotel.DTO;
using BookingHotel.Models;

namespace BookingHotel.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Branch, BranchDto>();
        }
    }
}
