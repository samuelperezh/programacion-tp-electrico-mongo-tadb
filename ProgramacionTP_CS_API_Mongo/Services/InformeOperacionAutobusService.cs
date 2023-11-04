using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class InformeOperacionAutobusService
    {
        private readonly IInformeOperacionAutobusRepository _InformeOperacionAutobusRepository;

        public InformeOperacionAutobusService(IInformeOperacionAutobusRepository InformeOperacionAutobusRepository)
        {
            _InformeOperacionAutobusRepository = InformeOperacionAutobusRepository;
        }

        public async Task<IEnumerable<InformeOperacionAutobus>> GetInformeOperacionAsync()
        {
            return await _InformeOperacionAutobusRepository
                .GetInformeOperacionAsync();
        }

        public async Task<InformeOperacionAutobus> GetInformeOperacionByIdAsync(int hora)
        {
            // Validamos que el informe utilizacion cargador exista
            var unInformeOperacionAutobus = await _InformeOperacionAutobusRepository
                .GetInformeOperacionByIdAsync(hora);

            if (unInformeOperacionAutobus.Hora != hora)
                throw new AppValidationException($"La hora {hora} no existe en la base de datos");

            return unInformeOperacionAutobus;
        }
    }
}