using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProgramacionTP_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeOperacionAutobusesController : Controller
    {
        private readonly InformeOperacionAutobusService _informeOperacionAutobusService;

        public InformeOperacionAutobusesController(InformeOperacionAutobusService informeOperacionAutobusService)
        {
            _informeOperacionAutobusService = informeOperacionAutobusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var elResumen = await _informeOperacionAutobusService
                .GetInformeOperacionAsync();

            return Ok(elResumen);
        }

        [HttpGet("{hora:int}")]
        public async Task<IActionResult> GetInformeOperacionByIdAsync(int hora)
        {
            try
            {
                var unInformeOperacionAutobus = await _informeOperacionAutobusService
                    .GetInformeOperacionByIdAsync(hora);
                return Ok(unInformeOperacionAutobus);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}