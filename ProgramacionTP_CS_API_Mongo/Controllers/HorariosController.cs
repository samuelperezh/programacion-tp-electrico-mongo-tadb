using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProgramacionTP_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : Controller
    {
        private readonly HorarioService _horarioService;

        public HorariosController(HorarioService horarioService)
        {
            _horarioService = horarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losHorarios = await _horarioService
                .GetAllAsync();

            return Ok(losHorarios);
        }

        [HttpGet("{hora:int}")]
        public async Task<IActionResult> GetByIdAsync(int hora)
        {
            try
            {
                var unHorario = await _horarioService
                    .GetByIdAsync(hora);
                return Ok(unHorario);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}