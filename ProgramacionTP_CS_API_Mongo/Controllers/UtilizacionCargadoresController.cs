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

        [HttpPut("{cargador_id:int}/{autobus_id:int}/{hora:int}")]
        public async Task<IActionResult> UpdateAsync(int cargador_id, int autobus_id, int hora, UtilizacionCargador unaUtilizacionCargador)
        {
            try
            {
                var utilizacionCargadorActualizado = await _utilizacionCargadorService
                    .UpdateAsync(cargador_id, autobus_id, hora, unaUtilizacionCargador);

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

        [HttpDelete("{cargador_id:int}/{autobus_id:int}/{hora:int}")]
        public async Task<IActionResult> DeleteAsync(int cargador_id, int autobus_id, int hora)
        {
            try
            {
                await _utilizacionCargadorService
                    .DeleteAsync(cargador_id, autobus_id, hora);

                return Ok($"Utilización del cargador {cargador_id} con el autobus {autobus_id} y en el horario {hora} fue eliminada");

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