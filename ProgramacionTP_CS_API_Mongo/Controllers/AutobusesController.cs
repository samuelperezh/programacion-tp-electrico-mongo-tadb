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

        [HttpGet("{codigo_autobus:int}")]
        public async Task<IActionResult> GetByIdAsync(int codigo_autobus)
        {
            try
            {
                var unAutobus = await _autobusService
                    .GetByIdAsync(codigo_autobus);
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

        [HttpPut("{codigo_autobus:int}")]
        public async Task<IActionResult> UpdateAsync(int codigo_autobus, Autobus unAutobus)
        {
            try
            {
                var autobusActualizado = await _autobusService
                    .UpdateAsync(codigo_autobus, unAutobus);

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

        [HttpDelete("{codigo_autobus:int}")]
        public async Task<IActionResult> DeleteAsync(int codigo_autobus)
        {
            try
            {
                await _autobusService
                    .DeleteAsync(codigo_autobus);

                return Ok($"Autobus {codigo_autobus} fue eliminada");

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

