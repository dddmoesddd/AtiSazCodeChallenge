using AKA.ReturnResult;
using CodeChallenge.Application.Command.Add;
using CodeChallenge.Utility.Exceptions;
using CodeChallenge.Utility.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableRateLimiting("fixed")]
    public class CodeChallengeController : AKAControllerBase
    {


        private readonly ILogger<CodeChallengeController> _logger;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly ICacheService _cacheService;
        public CodeChallengeController(ILogger<CodeChallengeController> logger, ICacheService cacheService, IMediator mediator, IConfiguration iConfig)
        {
            _logger = logger;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _configuration = iConfig;
            _cacheService = cacheService;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync([FromBody] TODOCommand command)
        {

            try
            {
                _logger.LogInformation($"AddAsync Called by User {new Random().Next()}", DateTime.UtcNow);
                var result = await _mediator.Send(command);
                _logger.LogInformation($"Data Added by User {new Random().Next()}", DateTime.UtcNow);
                return Ok(result);
            }
            catch (AddDataException ex)
            {
                _logger.LogInformation($"Exception in Add Data by  user {new Random().Next()}", DateTime.UtcNow);
                return ServerError(ex.Message);
            }


        }


        [HttpPost("GetAsync")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetAsync()
        {
            _logger.LogInformation($"GetAsync Called by User {new Random().Next()}", DateTime.UtcNow);
            string result = string.Empty;
            try
            {
             
                _logger.LogInformation("Get From Cache", DateTime.UtcNow);
                result = _cacheService.GetData("httpbin");
                if (result != null)
                {
                    return Ok(result);
                }

                else
                {
                    _logger.LogInformation("Calling httpbin", DateTime.UtcNow);
                    var client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync(_configuration.GetValue<string>("ExternalApi:httpbin"));
                    var expirationTime = DateTimeOffset.Now.AddSeconds(15.0);
                
                    response.EnsureSuccessStatusCode();
                     result = await response.Content.ReadAsStringAsync();

                     _cacheService.SetData("httpbin", JsonConvert.SerializeObject(result), expirationTime);
                }
     
           
                return Ok(result);
            }
            catch (HttpBinException ex)
            {
                _logger.LogInformation("Exception in Calling httpbin", DateTime.UtcNow);
                return ServerError(ex.Message);
            }


        }
    }
}