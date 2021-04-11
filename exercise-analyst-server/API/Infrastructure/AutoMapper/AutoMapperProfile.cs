using API.Domain.Models;
using API.Services.Auth.Dtos;
using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            MapsForAuth();
        }

        private void MapsForAuth()
        {
            CreateMap<ApplicationUser, LoginResponse>();
            
            CreateMap<ApplicationUser, RegisterResponse>();
        }
    }
}