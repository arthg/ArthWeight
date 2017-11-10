using ArthWeight.Data;
using ArthWeight.Data.Entities;
using ArthWeight.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ArthWeight.Controllers
{
    [Route("api/[Controller]")]
    public class WeightsController : Controller
    {
        private readonly IArthwindsRepository _arthwindsRepository;
        private readonly ILogger<WeightsController> _logger;

        public WeightsController(IArthwindsRepository arthwindsRepository,
            ILogger<WeightsController> logger)
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

        [HttpPost]
        public IActionResult Post([FromBody]WeightEntry weightEntry)
        {
            try
            {
                _arthwindsRepository.AddEntity(weightEntry);
                if (_arthwindsRepository.SaveAll())
                {
                    return Created($"/api/weights/{weightEntry.Id}", weightEntry);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save new weight: {ex}");
            }

            return BadRequest("Failed to save new weight");
        }
    }
}
