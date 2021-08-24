using AutoMapper;
using GorestApp.Business.Abstract.UserManager;
using GorestApp.Business.Abstract.Users;
using GorestApp.Core.Infrastructure;
using GorestApp.Core.Infrastructure.ErrorHandling;
using GorestApp.Core.Utilities.Helpers;
using GorestApp.DataAccess.Abstract;
using GorestApp.Entities.Concrete.Users;
using GorestApp.Entities.Dto.User;
using GorestApp.Entities.Dto.Users;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GorestApp.Business.Concrete.UserManager
{
    public class UserWriteManager : IUserWriteService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _worker;
        private readonly IUserReadService _userReadService;

        public UserWriteManager(IMapper mapper, IUnitOfWork worker, IUserReadService userReadService)
        {
            _mapper = mapper;
            _worker = worker;
            _userReadService = userReadService;
        }

        public async Task<UserDto.WithId> CreateAsync(UserDto.InsertOrUpdate userDto)
        {
            if (await _userReadService.GetUserByIdAsync(userDto.Id) != null)
                throw new RecordExistException(GorestAppMessages.AlreadyRegistered);

            var user = _mapper.Map<User>(userDto);
            user = _worker.UserRepository.Insert(user);
            await _worker.SaveChangesAsync();
            return _mapper.Map<UserDto.WithId>(user);
        }

        public async Task<bool> CreateRangeAsync(List<UserDto.InsertOrUpdate> userDtos)
        {
            foreach (var item in userDtos)
            {
                if (await _userReadService.GetUserByIdAsync(item.Id) != null) // eğer kayıt varsa bir sonraki kayda geç
                    continue;
                _worker.UserRepository.Insert(_mapper.Map<User>(item));
            } 
            await _worker.SaveChangesAsync();
            return true;
        }

        public async Task<UserDto.WithId> UpdateAsync(UserDto.InsertOrUpdate userDto)
        {
            var userEntity = await _worker.UserRepository.GetByIdAsync(userDto.Id);
            if (userEntity == null)
                throw new RecordNotFoundException(GorestAppMessages.RecordNotFound);

            if (userEntity.Email != userDto.Email && await _userReadService.UserExistsByEmailAsync(userDto.Email))
                throw new RecordExistException(GorestAppMessages.AlreadyRegistered);

            userEntity = _mapper.Map<User>(userDto);
            _worker.UserRepository.Update(userEntity);
            await _worker.SaveChangesAsync();
            return _mapper.Map<UserDto.WithId>(userEntity);
        }

        public async Task DeleteAsync(int Id)
        {
            var user = await _worker.UserRepository.FirstOrDefaultAsync(d => d.Id == Id);
            if (user == null)
                throw new RecordNotFoundException(GorestAppMessages.RecordNotFound);
            _worker.UserRepository.Delete(user);
            await _worker.SaveChangesAsync();
        }

        public async Task<bool> UpdateGorestUserSourceAsync()
        {
            List<UserDto.InsertOrUpdate> userDtos = new List<UserDto.InsertOrUpdate>();

            var client = new RestClient(AppConfigurationHelper.GetGorestUserSourceUrl());
            var request = new RestRequest("?page=1", Method.GET);
            var result = client.Get<UserStoreModel>(request);

            userDtos.AddRange(result.Data.Data.Select(d =>
            new UserDto.InsertOrUpdate
            {
                Id = d.Id,
                Name = d.Name,
                Email = d.Email,
                Gender = d.Gender,
                Status = d.Status
            }).ToList());

            int pageCount = result.Data.Meta.Pagination.Pages;
            for (int i = 2; i <= pageCount; i++)
            {
                request = new RestRequest($"?page={i}", Method.GET);
                result = client.Get<UserStoreModel>(request);

                userDtos.AddRange(result.Data.Data.Select(d =>
                new UserDto.InsertOrUpdate
                {
                    Id = d.Id,
                    Name = d.Name,
                    Email = d.Email,
                    Gender = d.Gender,
                    Status = d.Status
                }).ToList());

                System.Threading.Thread.Sleep(100); // Veri kaynağının engellememesi için
            }

            return await CreateRangeAsync(userDtos);
        }
    }
}
