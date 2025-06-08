using AutoMapper;
using WebAPI.Models.DTOs;
using Shared.forme; 

namespace WebAPI.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Forme, FormeDto>()
                .ConvertUsing(forme => new FormeDto(forme));
        }
    }
}     