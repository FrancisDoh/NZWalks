
// Goal: To automatically convert the Domain to DTO using AutoMapper 3rd party library back and forth.
using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile: Profile
    {
        // Cstructor
        public  RegionsProfile() {

            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                //.ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
                .ReverseMap();

        }
    }
}
