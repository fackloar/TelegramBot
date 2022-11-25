using ActualBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ActualBot.BotAPI
{
    public class BotAPI : IBotAPI
    {
        private static HttpClient _httpClient = new();
        private static readonly string _baseUrl = "https://localhost:7142/api";

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

        public async Task<UserDTO> GetUserByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/User/id/{id}");
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
    }
}
