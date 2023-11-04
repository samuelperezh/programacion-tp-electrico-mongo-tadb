using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProgramacionTP_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargadoresController : Controller
    {
        private readonly CargadorService _cargadorService;

        public CargadoresController(CargadorService cargadorService)
        {
            _cargadorService = cargadorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var losCargadores = await _cargadorService
                .GetAllAsync();

            return Ok(losCargadores);
        }

        [HttpGet("{cargador_id}")]
        public async Task<IActionResult> GetByIdAsync(string cargador_id)
        {
            try
            {
                var unCargador = await _cargadorService
                    .GetByIdAsync(cargador_id);
                return Ok(unCargador);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Cargador unCargador)
        {
            try
            {
                var cargadorCreado = await _cargadorService
                    .CreateAsync(unCargador);

                return Ok(cargadorCreado);
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

        [HttpPut("{cargador_id}")]
        public async Task<IActionResult> UpdateAsync(string cargador_id, Cargador unCargador)
        {
            try
            {
                var cargadorActualizado = await _cargadorService
                    .UpdateAsync(cargador_id, unCargador);

                return Ok(cargadorActualizado);

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

        [HttpDelete("{cargador_id}")]
        public async Task<IActionResult> DeleteAsync(string cargador_id)
        {
            try
            {
                await _cargadorService
                    .DeleteAsync(cargador_id);

                return Ok($"Cargador {cargador_id} fue eliminada");

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