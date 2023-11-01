using Microsoft.AspNetCore.Mvc;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Services;

namespace ProgramacionTP_CS_API_Mongo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeController : Controller
    {
        private readonly InformeService _informeService;

        public InformeController(InformeService informeService)
        {
            _informeService = informeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var elResumen = await _informeService
                .GetInformeAsync();

            return Ok(elResumen);
        }
    }
}
