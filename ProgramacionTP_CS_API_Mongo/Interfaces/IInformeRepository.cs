using ProgramacionTP_CS_API_Mongo.Models;
namespace ProgramacionTP_CS_API_Mongo.Interfaces
{
    public interface IInformeRepository
    {
        public Task<Informe> GetInformeAsync();
    }
}