using ActualBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using File = System.IO.File;

namespace ActualBot.BotAPI
{
    public class BotAPI : IBotAPI
    {
        private static HttpClient _httpClient = new();
        private static readonly string _baseUrl = File.ReadAllText("connectionString.txt");

        public async Task<HttpResponseMessage> CreateUserAsync(UserDTO user)
        {
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/User", stringContent);
            return response;
        }

        public async Task<List<UserDTO>> GetUsersOfChatAsync(long id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/User/chat/{id}");
            var content = await response.Content.ReadAsStringAsync();
            List<UserDTO> userDTOs = JsonConvert.DeserializeObject<List<UserDTO>>(content);
            return userDTOs;
        }

        public async Task<UserDTO> GetUserOfChatAsync(long chatId, long userId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/User/{chatId}/{userId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                UserDTO userDTO = JsonConvert.DeserializeObject<UserDTO>(content);
                return userDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<UserDTO>> GetUserByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/User/telegram/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<UserDTO> userDTO = JsonConvert.DeserializeObject<List<UserDTO>>(content);
                return userDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> CreateChatAsync(ChatDTO chat)
        {
            var json = JsonConvert.SerializeObject(chat);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Chat", stringContent);
            return response;
        }

        public async Task<ChatDTO> GetChatByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Chat/id/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                ChatDTO chatDTO = JsonConvert.DeserializeObject<ChatDTO>(content);
                return chatDTO;
            }
            else
            {
                return null;
            }
        }

        public async Task<HttpResponseMessage> UpdateUser(int id, UserDTO user)
        {
            var json = JsonConvert.SerializeObject(user);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/User/{id}", stringContent);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateChat(long id, ChatDTO chat)
        {
            var json = JsonConvert.SerializeObject(chat);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/Chat/{id}", stringContent);
            return response;
        }

        public async Task<List<UserDTO>> GetTopWinners(long chatId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/User/{chatId}/topWinners");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<UserDTO> userDTO = JsonConvert.DeserializeObject<List<UserDTO>>(content);
                userDTO.Reverse();
                return userDTO;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<UserDTO>> GetTopKarma(long chatId)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/User/{chatId}/topKarma");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<UserDTO> userDTO = JsonConvert.DeserializeObject<List<UserDTO>>(content);
                userDTO.Reverse();
                return userDTO;
            }
            else
            {
                return null;
            }
        }
    }
}
