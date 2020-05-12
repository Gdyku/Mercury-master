using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mercury.Api.ViewModels;
using Mercury.BLL.Dtos;
using Mercury.BLL.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mercury.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userService;
        private readonly ILogger _logger;

        public UserController(IUserLogic userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("Subscriptions")]
        public async Task<ActionResult> GetSubscribed()
        {
            var usersList = await _userService.GetSubscribingUsers();

            return Ok(usersList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(long id)
        {
            var response = new Response<UserDto>();

            try
            {
                response.Object = await _userService.GetUser(id);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "User with given id does not exist";

                _logger.LogError($"UserController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        [HttpGet("users")]
        public async Task<ActionResult> GetUsers(int itemPerPage, int page)
        {
            var response = new ResponseCollection<UserDto>();

            try
            {
                response.Objects = await _userService.GetUsers(itemPerPage, page);
            }
            catch(Exception e)
            {
                response.IsError = true;
                response.ErrorMessage = e.Message;

                _logger.LogError($"UserController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> NewUser([FromBody]NewUserDto user)
        {
            var response = new Response<UserDto>();

            try
            {
                response.Object = await _userService.CreateUser(user);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "Data provided by client is not valid";

                _logger.LogError($"UserController | Error | Error message : {e.Message}");
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            var response = new Response<UserDto>();

            try
            {
                response.Object = await _userService.UpdateUser(user);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "User with given id does not exist";

                _logger.LogError($"UserController | Error | Error message : {e.Message}");
            }

            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            var response = new Response<UserDto>();

            try
            {
                await _userService.DeleteUser(userId);
            }
            catch (InvalidOperationException e)
            {
                response.IsError = true;
                response.ErrorMessage = "User with given id does not exist";

                _logger.LogError($"UserController | Error | Error message : {e.Message}");
            }

            return Ok();
        }
    }
}