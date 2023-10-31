using ProgramacionTP_CS_API_Mongo.Models;
using MongoDB.Driver;

namespace ProgramacionTP_CS_API_Mongo.DbContexts
{
    public class MongoDbContext
    {
        private readonly string cadenaConexion;
        private readonly ProgramacionTPDatabaseSettings _ProgramacionTPDatabaseSettings;
        public MongoDbContext(IConfiguration unaConfiguracion)
        {
            cadenaConexion = unaConfiguracion.GetConnectionString("Mongo")!;
            _ProgramacionTPDatabaseSettings = new ProgramacionTPDatabaseSettings(unaConfiguracion);
        }

        public IMongoDatabase CreateConnection()
        {
            var clienteDB = new MongoClient(cadenaConexion);
            var miDB = clienteDB.GetDatabase(_ProgramacionTPDatabaseSettings.DatabaseName);

            return miDB;
        }

        public ProgramacionTPDatabaseSettings configuracionColecciones
        {
            get { return _ProgramacionTPDatabaseSettings; }
        }
    }
}