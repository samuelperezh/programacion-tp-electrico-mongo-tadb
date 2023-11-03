using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class InformeService
    {
        private readonly IInformeRepository _informeRepository;

        public InformeService(IInformeRepository informeRepository)
        {
            _informeRepository = informeRepository;
        }

        public async Task<Informe> GetInformeAsync()
        {
            return await _informeRepository
                .GetInformeAsync();
        }
    }
}

