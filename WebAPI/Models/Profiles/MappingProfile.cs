using AutoMapper;
using WebAPI.Models.DTOs;
using EzChess.forme; 

namespace WebAPI.Models.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Carre, CarreDto>();
            CreateMap<Rectangle, RectangleDto>();
            CreateMap<Cercle, CercleDto>();
            CreateMap<Triangle, TriangleDto>();
        }
    }
}