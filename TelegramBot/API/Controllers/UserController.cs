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
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/UserDTOes
        [HttpGet]
        public async Task<ActionResult<IList<UserDTO>>> GetAll()
        {
            var response = new GetUsersResponse()
            {
                Users = new List<UserDTO>()
            };
            var users = await _userService.GetAll();
            foreach (var user in users)
            {
                response.Users.Add(user);
            }
            return Ok(response.Users);

        }

        // GET: api/UserDTOes/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(long id)
        {
            var response = await _userService.GetById(id);
            return Ok(response);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<List<UserDTO>>> GetUserByName(string name)
        {
            var response = await _userService.GetByName(name);
            return Ok(response);
        }

        [HttpGet("chat/{id}")]
        public async Task<ActionResult<List<UserDTO>>> GetUsersFromChat(long id)
        {
            var response = new GetUsersResponse()
            {
                Users = new List<UserDTO>()
            };
            var users = await _userService.GetUsersOfChat(id);
            foreach (var user in users)
            {
                response.Users.Add(user);
            }
            return Ok(response.Users);

        }
        // PUT: api/UserDTOes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]long id, [FromBody] UserDTO userDTO)
        {
            await _userService.Update(id, userDTO);
            return Ok();
        }

        // POST: api/UserDTOes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Create([FromBody]UserDTO userDTO)
        {
                await _userService.Create(userDTO);
                return Ok();
        }

        // DELETE: api/UserDTOes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}
