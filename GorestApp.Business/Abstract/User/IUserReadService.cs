using GorestApp.Entities.Concrete.Users;
using GorestApp.Entities.Dto.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GorestApp.Business.Abstract.UserManager
{
    public interface IUserReadService
    {
        List<UserDto.WithId> GetUserList(UserFilterModel filter);
        Task<UserDto.WithId> GetUserByIdAsync(int userId);
        Task<bool> UserExistsByEmailAsync(string email);
        Task<List<UserDto.WithId>> GetUserListAsync(UserFilterModel filter);
    }
}
