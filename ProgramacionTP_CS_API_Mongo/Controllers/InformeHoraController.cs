using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;

namespace ProgramacionTP_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeHoraController : Controller
    {
        private readonly InformeHoraService _informeHoraService;

        public InformeHoraController(InformeHoraService informeHoraService)
        {
            _informeHoraService = informeHoraService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var elResumen = await _informeHoraService
                .GetAllInformeHoraAsync();

            return Ok(elResumen);
        }

        [HttpGet("{hora:int}")]
        public async Task<IActionResult> GetInformeHoraAsync(int hora)
        {
            try
            {
                var unInformeHora = await _informeHoraService
                    .GetInformeHoraAsync(hora);
                return Ok(unInformeHora);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}