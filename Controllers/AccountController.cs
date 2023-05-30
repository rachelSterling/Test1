using Microsoft.AspNetCore.Mvc;
using Test1.Models;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private const string RateLimitCacheKey = "RateLimitCounter";
        private const string LatestValidRequestCacheKey = "LatestRequest";
        private const int MinSecondsPerRequest = 3;


        public AccountController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpPost]
        public IActionResult Post(EmailRequest request)
        {
            var recievedTime = DateTime.Now;

            if (CheckRateLimit())
            {
                var errorResponse = new ErrorResponse
                {
                    Message = "To many Requests",
                    LastValidRequest = _cache.Get<EmailRequest>(LatestValidRequestCacheKey)
                };

                return StatusCode(429, errorResponse);
            }

            if (request == null)
                throw new ArgumentNullException("request is null");

            _cache.Set(LatestValidRequestCacheKey, request);
            return Ok(new EmailResponse
            {
                Email = request.Email,
                Date = recievedTime.ToString("HH:mm:ss")
            });
        }

        private bool CheckRateLimit()
        {
            int currentCounter = _cache.GetOrCreate(RateLimitCacheKey, entry => 0);
            currentCounter++;
            _cache.Set(RateLimitCacheKey, currentCounter, TimeSpan.FromSeconds(MinSecondsPerRequest));

            return currentCounter > 1;
        }
    }
}