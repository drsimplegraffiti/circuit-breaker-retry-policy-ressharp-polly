using AutoMapper;
using FormulaOne.Entities.DbSet;
using FormulaOne.Entities.Dtos.Requests;

namespace FormulaOne.Api.MappingProfiles
{
    public class RequestToDomain : Profile
    {
        public RequestToDomain()
        {
            //        destination, source = domain, request
            CreateMap<CreateDriverAchievementRequest, Achievement>()
                // .ForMember(dest => dest.DriverId, opt => opt.MapFrom(src => src.DriverId))
                // .ForMember(dest => dest.FastestLap, opt => opt.MapFrom(src => src.FastestLap))
                // .ForMember(dest => dest.PolePosition, opt => opt.MapFrom(src => src.PolePosition))
                // .ForMember(dest => dest.WorldChampionship, opt => opt.MapFrom(src => src.WorldChampionship))
                // we don't need to map the above properties because they have the same name

                // we can also use the below code to map the properties with the same name
                .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));



             //        destination, source = domain, request
            CreateMap<UpdateDriverAchievementRequest, Achievement>()
                .ForMember(dest => dest.RaceWins, opt => opt.MapFrom(src => src.Wins))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));


            CreateMap<CreateDriverRequest, Driver>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.AddedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateDriverRequest, Driver>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.UtcNow));
                
        }

    }
}