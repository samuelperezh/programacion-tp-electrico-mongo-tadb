using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;

namespace ProgramacionTB_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeUtilizacionCargadoresController : Controller
    {
        private readonly InformeUtilizacionCargadorService _informeUtilizacionCargadorService;

        public InformeUtilizacionCargadoresController(InformeUtilizacionCargadorService informeUtilizacionCargadorService)
        {
            _informeUtilizacionCargadorService = informeUtilizacionCargadorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetInformeUtilizacionAsync()
        {
            var elResumen = await _informeUtilizacionCargadorService
                .GetInformeUtilizacionAsync();

            return Ok(elResumen);
        }

        [HttpGet("{hora:int}")]
        public async Task<IActionResult> GetInformeUtilizacionByIdAsync(int hora)
        {
            try
            {
                var unInformeUtilizacionCargador = await _informeUtilizacionCargadorService
                    .GetInformeUtilizacionByIdAsync(hora);
                return Ok(unInformeUtilizacionCargador);
            }
            catch (AppValidationException error)
            {
                return NotFound(error.Message);
            }
        }
    }
}