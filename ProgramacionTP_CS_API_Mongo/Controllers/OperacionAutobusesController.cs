using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;

namespace ProgramacionTP_CS_API_Mongo.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OperacionAutobusesController : Controller
    {
        private readonly OperacionAutobusService _operacionAutobusService;

        public OperacionAutobusesController(OperacionAutobusService operacionAutobusService)
        {
            _operacionAutobusService = operacionAutobusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losOperacionAutobuses = await _operacionAutobusService
                .GetAllAsync();

            return Ok(losOperacionAutobuses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OperacionAutobus unaOperacionAutobus)
        {
            try
            {
                var operacionAutobusCreado = await _operacionAutobusService
                    .CreateAsync(unaOperacionAutobus);

                return Ok(operacionAutobusCreado);
            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpPut("{codigo_autobus:int}/{horario_id:int}")]
        public async Task<IActionResult> UpdateAsync(int codigo_autobus, int horario_id, OperacionAutobus unaOperacionAutobus)
        {
            try
            {
                var operacionAutobusActualizado = await _operacionAutobusService
                    .UpdateAsync(codigo_autobus, horario_id, unaOperacionAutobus);

                return Ok(operacionAutobusActualizado);

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }

        [HttpDelete("{codigo_autobus:int}/{horario_id:int}")]
        public async Task<IActionResult> DeleteAsync(int codigo_autobus, int horario_id)
        {
            try
            {
                await _operacionAutobusService
                    .DeleteAsync(codigo_autobus, horario_id);

                return Ok($"La operacion del autobus {codigo_autobus} en el horario {horario_id} fue eliminada");

            }
            catch (AppValidationException error)
            {
                return BadRequest($"Error de validación: {error.Message}");
            }
            catch (DbOperationException error)
            {
                return BadRequest($"Error de operacion en DB: {error.Message}");
            }
        }
    }
}