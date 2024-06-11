
using AutoMapper;
using Domain.Entities;
using Services.Models.User;

namespace Services.MapperProfiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            Init();
        }

        private void Init()
        {
            CreateMap<AppUser, UserForView>();
        }
    }
}
