using AutoMapper;
using backend.DTOs;
using backend.Models;

namespace backend.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserModel>()
                .ReverseMap();
        }
    }
}