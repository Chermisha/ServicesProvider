using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServicesProvider.Models;
using ServicesProvider.Services;
using System.ComponentModel.DataAnnotations;

namespace ServicesProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController:ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(string email, string password)
        {
            var result = await _usersService.Register(email, password);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<StatusCode>> Login(string email, string password)
        {
            var result = await _usersService.Login(email, password);

            var resultStatus = result.Item1;

            var token = result.Item2;

            if(token!=null)
                HttpContext.Response.Cookies.Append("bearer", token);

            return Ok(resultStatus);
            
        }
    }
}
