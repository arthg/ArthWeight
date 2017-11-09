using ArthWeight.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ArthWeight.Controllers
{
    [Route("api/[Controller]")]
    public class WeightsApiController : Controller
    {
        private readonly IArthwindsRepository _arthwindsRepository;
        private readonly ILogger<WeightsApiController> _logger;

        public WeightsApiController(IArthwindsRepository arthwindsRepository,
            ILogger<WeightsApiController> logger)
        {
            _arthwindsRepository = arthwindsRepository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_arthwindsRepository.GetWeightEntries());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get weight entries: {ex}");
                return BadRequest("Failed to get weight entries");
            }
        }
    }
}
