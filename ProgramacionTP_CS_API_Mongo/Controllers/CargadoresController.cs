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

        [HttpGet("{codigo_cargador:int}")]
        public async Task<IActionResult> GetByIdAsync(int codigo_cargador)
        {
            try
            {
                var unCargador = await _cargadorService
                    .GetByIdAsync(codigo_cargador);
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

        [HttpPut("{codigo_cargador:int}")]
        public async Task<IActionResult> UpdateAsync(int codigo_cargador, Cargador unCargador)
        {
            try
            {
                var cargadorActualizado = await _cargadorService
                    .UpdateAsync(codigo_cargador, unCargador);

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

        [HttpDelete("{codigo_cargador:int}")]
        public async Task<IActionResult> DeleteAsync(int codigo_cargador)
        {
            try
            {
                await _cargadorService
                    .DeleteAsync(codigo_cargador);

                return Ok($"Cargador {codigo_cargador} fue eliminada");

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