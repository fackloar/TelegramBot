using AutoMapper;
using TelegramBot.BusinessLayer.DTOs;
using TelegramBot.BusinessLayer.Interfaces;
using TelegramBot.DataLayer.Interfaces;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private IUserDBRepository _repository;
        private IMapper _mapper;

        public UserService(IUserDBRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(UserDTO entity)
        {
            var userToCreate = _mapper.Map<UserDB>(entity);
            await _repository.Create(userToCreate);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<IList<UserDTO>> GetAll()
        {
            var users = await _repository.GetAll();
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (UserDB userDB in users)
            {
                UserDTO convertedUser = _mapper.Map<UserDTO>(userDB);
                userDTOs.Add(convertedUser);
            }
            return userDTOs;
        }

        public async Task<UserDTO> GetById(long id)
        {
            var userGot = await _repository.GetById(id);
            var userDTO = _mapper.Map<UserDTO>(userGot);
            return userDTO;
        }

        public async Task<IList<UserDTO>> GetByName(string name)
        {
            var users = await _repository.GetByName(name);
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (UserDB userDB in users)
            {
                UserDTO convertedUser = _mapper.Map<UserDTO>(userDB);
                userDTOs.Add(convertedUser);
            }
            return userDTOs;
        }

        public async Task Update(long id, UserDTO entity)
        {
            var updatedUser = _mapper.Map<UserDB>(entity);
            await _repository.Update(id, updatedUser);
        }

        public async Task<IList<UserDTO>> GetUsersOfChat(long chatId)
        {
            var users = await _repository.GetUsersOfChat(chatId);
            List<UserDTO> userDTOs = new List<UserDTO>();
            foreach (UserDB userDB in users)
            {
                UserDTO convertedUser = _mapper.Map<UserDTO>(userDB);
                userDTOs.Add(convertedUser);
            }
            return userDTOs;
        }
    }
}
