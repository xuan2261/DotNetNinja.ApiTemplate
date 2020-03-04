using AutoMapper;
using DotNetNinja.ApiTemplate.Controllers.v1;
using DotNetNinja.ApiTemplate.Domain;

namespace DotNetNinja.ApiTemplate.App
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<State, StateModel>().ReverseMap();
        }
    }
}