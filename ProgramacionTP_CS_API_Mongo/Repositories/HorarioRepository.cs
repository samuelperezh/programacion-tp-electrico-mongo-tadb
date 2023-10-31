using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using MongoDB.Driver;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class HorarioRepository
    {
        namespace ProgramacionTP_CS_API_PostgreSQL_Dapper.Repositories
    {
        public class HorarioRepository : IHorarioRepository
        {
            private readonly MongoDbContext contextoDB;

            public HorarioRepository(MongoDbContext unContexto)
            {
                contextoDB = unContexto;
            }
        }
}
