using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesProvider.Domain.Models;
using ServicesProvider.Application.Services;

namespace ServicesProvider.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestsController : Controller
    {
        private readonly IRequestsService _requestsService;

        public RequestsController(IRequestsService requestsService)
        {
            _requestsService = requestsService;
        }

        [HttpGet]
        [Authorize(Policy = "ProviderPolicy")]
        public async Task<ActionResult<List<Request>>> GetRequests()
        {
            //var requests = await _requestsService.GetAllRequests();

            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = "ClientPolicy")]
        public async Task<ActionResult<List<Request>>> AddRequest()
        {
            //var requests = await _requestsService.GetAllRequests();

            return Ok();
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Request>> GetRequest([FromRoute]int id)
        //{
        //    var request = await _requestsService.GetRequestById(id);
        //    return Ok(request);
        //}

        //[HttpPost]
        //public async Task<ActionResult> CreateRequest([FromBody] int userId, int serviceId, string? comment)
        //{

        //    var request = _requestsService.CreateRequest()
        //}
    }
}
