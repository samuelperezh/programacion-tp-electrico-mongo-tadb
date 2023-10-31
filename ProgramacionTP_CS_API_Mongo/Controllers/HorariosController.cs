using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Services;

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

        [HttpGet("{horario_id:int}")]
        public async Task<IActionResult> GetByIdAsync(int horario_id)
        {
            try
            {
                var unHorario = await _horarioService
                    .GetByIdAsync(horario_id);
                return Ok(unHorario);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
