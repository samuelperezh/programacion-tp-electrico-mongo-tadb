using ProgramacionTP_CS_API_Mongo.Repositories;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Helpers;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class InformeHoraService
    {
        private readonly IInformeHoraRepository _informeHoraRepository;

        public InformeHoraService(IInformeHoraRepository informeHoraRepository)
        {
            _informeHoraRepository = informeHoraRepository;
        }

        public async Task<IEnumerable<InformeHora>> GetAllInformeHoraAsync()
        {
            return await _informeHoraRepository
                .GetAllInformeHoraAsync();
        }

        public async Task<InformeHora> GetInformeHoraAsync(int hora)
        {
            // Validamos que el informeHora exista con ese Id
            var unInformeHora = await _informeHoraRepository
                .GetInformeHoraAsync(hora);

            if (unInformeHora.Hora < 0 || unInformeHora.Hora > 23)
                throw new AppValidationException($"La hora no fue encontrada con el id {hora}");

            return unInformeHora;
        }
    }
}