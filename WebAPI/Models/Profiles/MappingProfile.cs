using AutoMapper;
using WebAPI.Models.DTOs;
using EzChess.forme; 

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