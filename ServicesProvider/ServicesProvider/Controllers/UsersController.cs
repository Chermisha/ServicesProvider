using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using ServicesProvider.Domain.Models;
using ServicesProvider.Application.Services;
using ServicesProvider.Models;
using ServicesProvider.Domain.Enums;

namespace ServicesProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("register")]
        public async Task<ActionResult> Register()
        {
            return View("Register");
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm]string email, [FromForm] string password, [FromForm] string confirmPassword) 
        {
            if (password == confirmPassword)
            {
                var result = await _usersService.Register(email, password);
                return View("Login");
            }
            else return BadRequest();
        }

        [HttpGet("login")]
        public async Task<ActionResult> Login()
        {
            return View("Login");
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseBase>> Login([FromForm] string email, [FromForm] string password)
        {
            var result = await _usersService.Login(email, password);

            var resultStatus = "Код ошибки: "+ result.Code + " " + result.Description;

            var token = result.Data.Item2;

            var user = result.Data.Item1;

            if (token != null)
                HttpContext.Response.Cookies.Append("bearer", token);

            if (user != null)
            {
                if (user.Role == UserRole.Provider)
                {
                    return RedirectToAction("Provider", "Home");
                }
                else if (user.Role == UserRole.Client)
                {
                    return RedirectToAction("Client", "Home");
                }
                else
                {
                    return BadRequest(resultStatus);
                }
            }
            else
            {
                return BadRequest(resultStatus);
            }
        }

        [HttpGet("logout")]
        public async Task<ActionResult> Logout()
        {
            HttpContext.Response.Cookies.Delete("bearer");
            return RedirectToAction("Login");
        }
    
    
    }
}
