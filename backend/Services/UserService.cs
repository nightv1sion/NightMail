using AutoMapper;
using Contracts;
using Entities.ExceptionModels;
using Services.Contracts;
using Shared.DataTransferObjects;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private IRepositoryManager _repository;
        private IMapper _mapper;
        public UserService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public T GetUserById<T>(Guid id, bool trackChanges)
        {
            var user = _repository.UserRepository.GetUserById(id, trackChanges);
            if (user == null)
                throw new UserNotFoundException(id);

            var userDto = _mapper.Map<T>(user);

            return userDto;
        }

        public async Task UpdateUserAsync(UserForUpdateDTO userDto)
        {
            var userEntity = _repository.UserRepository.GetUserById(userDto.Id, true);
            if(userEntity == null)
                throw new UserNotFoundException(userDto.Id);

            _mapper.Map(userDto, userEntity);
            await _repository.SaveAsync();
        }
    }
}
