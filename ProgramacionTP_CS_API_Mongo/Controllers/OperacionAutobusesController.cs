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

        [HttpPut("{autobus_id:length(24)}/{hora:int}")]
        public async Task<IActionResult> UpdateAsync(string autobus_id, int hora, OperacionAutobus unaOperacionAutobus)
        {
            try
            {
                var operacionAutobusActualizado = await _operacionAutobusService
                    .UpdateAsync(autobus_id, hora, unaOperacionAutobus);

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

        [HttpDelete("{autobus_id:length(24)}/{hora:int}")]
        public async Task<IActionResult> DeleteAsync(string autobus_id, int hora)
        {
            try
            {
                await _operacionAutobusService
                    .DeleteAsync(autobus_id, hora);

                return Ok($"La operacion del autobus {autobus_id} en el horario {hora} fue eliminada");

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