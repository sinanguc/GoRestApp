using AutoMapper;
using GorestApp.Entities.Concrete.Users;
using GorestApp.Entities.Dto.Users;
using System.Collections.Generic;

namespace GorestApp.Business.Configuration
{
    public static class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserDto, User>();
                config.CreateMap<User, UserDto>();
                config.CreateMap<UserDto.WithId, User>();
                config.CreateMap<User, UserDto.WithId>();
            });

            return mappingConfig;
        }
    }
}
