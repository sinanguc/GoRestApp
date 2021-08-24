using AutoMapper;
using GorestApp.Business.Abstract.UserManager;
using GorestApp.DataAccess.Abstract;
using GorestApp.Entities.Concrete.Users;
using GorestApp.Entities.Dto.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorestApp.Business.Concrete.UserManager
{
    public class UserReadManager : IUserReadService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _worker;

        public UserReadManager(IMapper mapper, IUnitOfWork worker)
        {
            _mapper = mapper;
            _worker = worker;
        }

        public List<UserDto.WithId> GetUserList(UserFilterModel filter)
        {
            var userList = _worker.UserRepository.GetAll(d => d.Deleted == filter.IsDeleted);
            return _mapper.Map<List<UserDto.WithId>>(userList);
        }


        public async Task<UserDto.WithId> GetUserByIdAsync(int userId)
        {
            var user = await _worker.UserRepository.FirstOrDefaultAsync(d => d.Id == userId);
            return _mapper.Map<UserDto.WithId>(user);
        }

        public async Task<bool> UserExistsByEmailAsync(string email)
        {
            return await _worker.UserRepository.AnyAsync(d => d.Email.ToLower() == email.ToLower().Trim());
        }

        public async Task<List<UserDto.WithId>> GetUserListAsync(UserFilterModel filter)
        {
            var userList = await _worker.UserRepository.GetAllAsync(d => d.Deleted == filter.IsDeleted);
            return await Task.FromResult(_mapper.Map<List<UserDto.WithId>>(userList));
        }
    }
}
