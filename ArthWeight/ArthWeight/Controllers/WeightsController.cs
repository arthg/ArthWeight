using ArthWeight.Data;
using ArthWeight.Data.Entities;
using ArthWeight.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ArthWeight.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeightsController : Controller
    {
        private readonly IArthwindsRepository _arthwindsRepository;
        private readonly ILogger<WeightsController> _logger;
        private readonly IMapper _mapper;

        public WeightsController(IArthwindsRepository arthwindsRepository,
            ILogger<WeightsController> logger, IMapper mapper)
        {
            _arthwindsRepository = arthwindsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var username = User.Identity.Name;
                return Ok(_arthwindsRepository.GetWeightEntries());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get weight entries: {ex}");
                return BadRequest("Failed to get weight entries");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]WeightViewModel weightViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    var weightEntry = _mapper.Map<WeightViewModel, WeightEntry>(weightViewModel);
                    _arthwindsRepository.AddEntity(weightEntry);
                    if (_arthwindsRepository.SaveAll())
                    {
                        return Created($"/api/weights/{weightEntry.Id}", weightEntry);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
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
