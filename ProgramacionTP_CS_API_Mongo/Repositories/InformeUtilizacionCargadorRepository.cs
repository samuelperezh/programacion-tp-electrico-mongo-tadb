using MongoDB.Bson;
using MongoDB.Driver;
using ProgramacionTP_CS_API_Mongo.DbContexts;
using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using System.Data;

namespace ProgramacionTP_CS_API_Mongo.Repositories
{
    public class InformeUtilizacionCargadorRepository : IInformeUtilizacionCargadorRepository
    {
        private readonly MongoDbContext contextoDB;

        public InformeUtilizacionCargadorRepository(MongoDbContext unContexto)
        {
            contextoDB = unContexto;
        }
        // public async Task<IEnumerable<InformeUtilizacionCargador>> GetInformeUtilizacionAsync()
        // {
        //     using (var conexion = contextoDB.CreateConnection())
        //     {
        //         string sentenciaSQL = "SELECT * FROM v_total_utilizacion_cargadores";

        //         var unInformeUtilizacionCargador = await conexion.QueryAsync<InformeUtilizacionCargador>(sentenciaSQL,
        //                                 new DynamicParameters());

        //         return unInformeUtilizacionCargador;
        //     }
        // }

        // public async Task<InformeUtilizacionCargador> GetInformeUtilizacionByIdAsync(int hora)
        // {
        //     InformeUtilizacionCargador unInformeUtilizacionCargador = new InformeUtilizacionCargador();

        //     using (var conexion = contextoDB.CreateConnection())
        //     {
        //         DynamicParameters parametrosSentencia = new DynamicParameters();
        //         parametrosSentencia.Add("@hora", hora,
        //                                 DbType.Int32, ParameterDirection.Input);

        //         string sentenciaSQL = "SELECT * FROM v_total_utilizacion_cargadores WHERE hora = @hora";

        //         var resultado = await conexion.QueryAsync<InformeUtilizacionCargador>(sentenciaSQL,
        //                             parametrosSentencia);

        //         if (resultado.Count() > 0)
        //             unInformeUtilizacionCargador = resultado.First();
        //     }

        //     return unInformeUtilizacionCargador;
        // }
    }
}
