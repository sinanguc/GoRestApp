using GorestApp.Entities.Dto.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GorestApp.Business.Abstract.Users
{
    public interface IUserWriteService
    {
        Task<UserDto.WithId> CreateAsync(UserDto.InsertOrUpdate userDto);
        Task<bool> CreateRangeAsync(List<UserDto.InsertOrUpdate> userDtos);

        Task<UserDto.WithId> UpdateAsync(UserDto.InsertOrUpdate userDto);

        Task DeleteAsync(int Id);

        Task<bool> UpdateGorestUserSourceAsync();
    }
}
