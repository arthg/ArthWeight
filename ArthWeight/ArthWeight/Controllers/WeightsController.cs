using ArthWeight.Data;
using ArthWeight.Data.Entities;
using ArthWeight.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ArthWeight.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class WeightsController : Controller
    {
        private readonly IArthwindsRepository _arthwindsRepository;
        private readonly ILogger<WeightsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public WeightsController(IArthwindsRepository arthwindsRepository,
            ILogger<WeightsController> logger, IMapper mapper,
            UserManager<User> userManager)
        {
            _arthwindsRepository = arthwindsRepository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var username = User.Identity.Name;
                // TODO: should be mapper to view model
                var weightEntries = _arthwindsRepository.GetWeightEntries();
                return Ok(weightEntries);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get weight entries: {ex}");
                return BadRequest("Failed to get weight entries");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]WeightViewModel weightViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {                   
                    var weightEntry = _mapper.Map<WeightViewModel, WeightEntry>(weightViewModel);

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    weightEntry.User = currentUser;

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
