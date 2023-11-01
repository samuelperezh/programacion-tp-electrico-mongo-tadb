using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;

namespace ProgramacionTP_CS_API_PostgreSQL_Dapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutobusesController : Controller
    {
        private readonly AutobusService _autobusService;

        public AutobusesController(AutobusService autobusService)
        {
            _autobusService = autobusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losAutobuses = await _autobusService
                .GetAllAsync();

            return Ok(losAutobuses);
        }

        [HttpGet("{autobus_id:length(24)}")]
        public async Task<IActionResult> GetByIdAsync(string autobus_id)
        {
            try
            {
                var unAutobus = await _autobusService
                    .GetByIdAsync(autobus_id);
                return Ok(unAutobus);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Autobus unAutobus)
        {
            try
            {
                var autobusCreado = await _autobusService
                    .CreateAsync(unAutobus);

                return Ok(autobusCreado);
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

        [HttpPut("{autobus_id:length(24)}")]
        public async Task<IActionResult> UpdateAsync(string autobus_id, Autobus unAutobus)
        {
            try
            {
                var autobusActualizado = await _autobusService
                    .UpdateAsync(autobus_id, unAutobus);

                return Ok(autobusActualizado);

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

        [HttpDelete("{autobus_id:length(24)}")]
        public async Task<IActionResult> DeleteAsync(string autobus_id)
        {
            try
            {
                await _autobusService
                    .DeleteAsync(autobus_id);

                return Ok($"Autobus {autobus_id} fue eliminada");

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

