using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class InformeOperacionAutobusRepository : IInformeOperacionAutobusRepository
    {
        private readonly MongoDbContext contextoDB;

        public InformeOperacionAutobusRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }
    }
}