using AutoMapper;
using TelegramBot.BusinessLayer.DTOs;
using TelegramBot.BusinessLayer.Interfaces;
using TelegramBot.DataLayer.Interfaces;
using TelegramBot.DataLayer.Models;

namespace TelegramBot.BusinessLayer.Services
{
    public class ChatService : IChatService
    {
        private IChatDBRepository _repository;
        private IMapper _mapper;

        public ChatService(IChatDBRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(ChatDTO entity)
        {
            var chatToCreate = _mapper.Map<ChatDB>(entity);
            await _repository.Create(chatToCreate);
        }

        public async Task Delete(long id)
        {
            await _repository.Delete(id);
        }

        public async Task<IList<ChatDTO>> GetAll()
        {
            var chats = await _repository.GetAll();
            List<ChatDTO> chatDTOs = new List<ChatDTO>();
            foreach (ChatDB chatDB in chats)
            {
                ChatDTO convertedChat = _mapper.Map<ChatDTO>(chatDB);
                chatDTOs.Add(convertedChat);
            }
            return chatDTOs;
        }

        public async Task<ChatDTO> GetById(long id)
        {
            var chatGot = await _repository.GetById(id);
            var chatDTO = _mapper.Map<ChatDTO>(chatGot);
            return chatDTO;
        }

        public async Task<IList<ChatDTO>> GetByName(string name)
        {
            var chats = await _repository.GetByName(name);
            List<ChatDTO> chatDTOs = new List<ChatDTO>();
            foreach (ChatDB chatDB in chats)
            {
                ChatDTO convertedChat = _mapper.Map<ChatDTO>(chatDB);
                chatDTOs.Add(convertedChat);
            }
            return chatDTOs;
        }

        public async Task Update(long id, ChatDTO entity)
        {
            var updatedChat = _mapper.Map<ChatDB>(entity);
            await _repository.Update(id, updatedChat);
        }
    }
}
