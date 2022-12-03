using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelegramBot.API.Responses;
using TelegramBot.BusinessLayer.DTOs;
using TelegramBot.BusinessLayer.Interfaces;
using TelegramBot.DataLayer;

namespace TelegramBot.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ChatDTO>>> GetAll()
        {
            var response = new GetChatsResponse()
            {
                Chats = new List<ChatDTO>()
            };
            var chats = await _chatService.GetAll();
            foreach (var chat in chats)
            {
                response.Chats.Add(chat);
            }
            return Ok(response.Chats);

        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<ChatDTO>> GetChatById(long id)
        {
            var response = await _chatService.GetById(id);
            return Ok(response);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<ChatDTO>>> GetChatByName(string name)
        {
            var response = await _chatService.GetByName(name);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] long id, [FromBody] ChatDTO chatDTO)
        {
            await _chatService.Update(id, chatDTO);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<ChatDTO>> Create([FromBody] ChatDTO chatDTO)
        {
            await _chatService.Create(chatDTO);
            return Ok();
        }

        // DELETE: api/ChatDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _chatService.Delete(id);
            return Ok();
        }
    }
}
