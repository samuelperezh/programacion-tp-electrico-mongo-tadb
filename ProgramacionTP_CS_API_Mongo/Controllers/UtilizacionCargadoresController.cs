using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;

namespace ProgramacionTB_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizacionCargadoresController : Controller
    {
        private readonly UtilizacionCargadorService _utilizacionCargadorService;

        public UtilizacionCargadoresController(UtilizacionCargadorService utilizacionCargadorService)
        {
            _utilizacionCargadorService = utilizacionCargadorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var lasUtilizacionesCargadores = await _utilizacionCargadorService
                .GetAllAsync();

            return Ok(lasUtilizacionesCargadores);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UtilizacionCargador unaUtilizacionCargador)
        {
            try
            {
                var utilizacionCargadorCreado = await _utilizacionCargadorService
                    .CreateAsync(unaUtilizacionCargador);

                return Ok(utilizacionCargadorCreado);
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

        [HttpPut("{codigo_cargador:int}/{codigo_autobus:int}/{hora:int}")]
        public async Task<IActionResult> UpdateAsync(int codigo_cargador, int codigo_autobus, int hora, UtilizacionCargador unaUtilizacionCargador)
        {
            try
            {
                var utilizacionCargadorActualizado = await _utilizacionCargadorService
                    .UpdateAsync(codigo_cargador, codigo_autobus, hora, unaUtilizacionCargador);

                return Ok(utilizacionCargadorActualizado);

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

        [HttpDelete("{codigo_cargador:int}/{codigo_autobus:int}/{hora:int}")]
        public async Task<IActionResult> DeleteAsync(int codigo_cargador, int codigo_autobus, int hora)
        {
            try
            {
                await _utilizacionCargadorService
                    .DeleteAsync(codigo_cargador, codigo_autobus, hora);

                return Ok($"Utilización del cargador {codigo_cargador} con el autobus {codigo_autobus} y en el horario {hora} fue eliminada");

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