using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateApp.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ProfileService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<ProfileDto> GetProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var profileDto = _mapper.Map<ProfileDto>(user);
            profileDto.UserId = userId;
            var roles = await _userManager.GetRolesAsync(user);
            profileDto.Roles = roles.ToList();

            return profileDto;
        }

        public async Task<bool> UpdateProfileAsync(string userId, ProfileDto profileDto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            user.FirstName = profileDto.FirstName;
            user.LastName = profileDto.LastName;
            user.PhoneNumber = profileDto.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
