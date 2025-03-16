using AutoMapper;
using WebAPI.Models.DTOs;
using EzChess.forme; 

namespace WebAPI.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Carre, CarreDto>()
                .ForMember(dest => dest.Perimetre, opt => opt.MapFrom(src => src.GetPerimetre()))
                .ForMember(dest => dest.Aire, opt => opt.MapFrom(src => src.GetAire()));

            CreateMap<Rectangle, RectangleDto>()
                .ForMember(dest => dest.Perimetre, opt => opt.MapFrom(src => src.GetPerimetre()))
                .ForMember(dest => dest.Aire, opt => opt.MapFrom(src => src.GetAire()));

            CreateMap<Cercle, CercleDto>()
                .ForMember(dest => dest.Perimetre, opt => opt.MapFrom(src => src.GetPerimetre()))
                .ForMember(dest => dest.Aire, opt => opt.MapFrom(src => src.GetAire()));

            CreateMap<Triangle, TriangleDto>()
                .ForMember(dest => dest.Perimetre, opt => opt.MapFrom(src => src.GetPerimetre()))
                .ForMember(dest => dest.Aire, opt => opt.MapFrom(src => src.GetAire()));
        }
    }
}