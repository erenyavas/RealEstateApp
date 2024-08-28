using System.Threading.Tasks;
using RealEstateApp.DTOs;

namespace RealEstateApp.Services
{
    public interface IProfileService
    {
        Task<ProfileDto> GetProfileAsync(string userId);
        Task<bool> UpdateProfileAsync(string userId, ProfileDto profileDto);
    }
}
