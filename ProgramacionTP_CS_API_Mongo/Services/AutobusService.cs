using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Services
{

    public class AutobusService
    {
        private readonly IAutobusRepository _autobusRepository;

        public AutobusService(IAutobusRepository autobusRepository)
        {
            _autobusRepository = autobusRepository;
        }

        public async Task<IEnumerable<Autobus>> GetAllAsync()
        {
            return await _autobusRepository
                .GetAllAsync();
        }

        public async Task<Autobus> GetByIdAsync(int codigo_autobus)
        {
            // Validamos que el autobus exista con ese Id
            var unAutobus = await _autobusRepository
                .GetByIdAsync(codigo_autobus);
            if(unAutobus.CodigoAutobus == 0)
                throw new AppValidationException($"No existe un autobus con el Id {codigo_autobus}");

            return unAutobus;
        }

        public async Task<Autobus> CreateAsync(Autobus unAutobus)
        {
            // Validamos que el autobus tenga nombre

            if (unAutobus.Nombre_autobus.Length == 0)
                throw new AppValidationException("No se puede insertar un autobus con nombre nulo");

            // validamos que el autobus a crear no esté previamente creado
            var autobusExistente = await _autobusRepository
                .GetByNameAsync(unAutobus.Nombre_autobus!);

            if(autobusExistente.CodigoAutobus != 0)
                throw new AppValidationException($"Ya existe un autobus con el nombre {unAutobus.Nombre_autobus}");

            try
            {
                bool resultadoAccion = await _autobusRepository
                    .CreateAsync(unAutobus);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                unAutobus = await _autobusRepository
                    .GetByNameAsync(unAutobus.Nombre_autobus!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return autobusExistente;
        }

        public async Task<Autobus> UpdateAsync(int codigo_autobus, Autobus unAutobus)
        {
            // Validamos que los parámetros sean consistentes
            if (codigo_autobus != unAutobus.CodigoAutobus)
                throw new AppValidationException($"Inconsistencia en el Id del autobus a actualizar. Verifica argumentos");

            // Validamos que el autobus tenga nombre
            if (unAutobus.Nombre_autobus.Length == 0)
                throw new AppValidationException("No se puede actualizar un autobus con nombre nulo");

            // Validamos que el nuevo nombre no exista previamente en otro autobus diferente al que se estó actualizando
           var autobusExistente = await _autobusRepository
                .GetByNameAsync(unAutobus.Nombre_autobus);

            if (!string.IsNullOrEmpty(autobusExistente.Id))
                throw new AppValidationException($"Ya existe un autobus con el nombre {unAutobus.Nombre_autobus}");

            // validamos que el autobus a actualizar si exista con ese Id
            autobusExistente = await _autobusRepository
                .GetByIdAsync(unAutobus.CodigoAutobus);

            if (string.IsNullOrEmpty(autobusExistente.Id))
                throw new AppValidationException($"No existe un autobus con el Id {unAutobus.CodigoAutobus} que se pueda actualizar");

            try
            {
                bool resultadoAccion = await _autobusRepository
                    .UpdateAsync(unAutobus);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                autobusExistente = await _autobusRepository
                    .GetByNameAsync(unAutobus.Nombre_autobus!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return autobusExistente;
        }

        public async Task DeleteAsync(int codigo_autobus)
        {
            // Validamos que el autobus a eliminar si exista con ese Id
            var autobusExistente = await _autobusRepository
                .GetByIdAsync(codigo_autobus);

            if (string.IsNullOrEmpty(autobusExistente.Id))
                throw new AppValidationException($"No existe un autobus con el Id {codigo_autobus} que se pueda eliminar");

            // Validamos que el autobus no tenga asociadas utilizaciones de cargadores
            var cantidadUtilizacionCargadoresAsociados = await _autobusRepository
                .GetTotalAssociatedChargerUtilizationAsync(autobusExistente.CodigoAutobus);

            if (cantidadUtilizacionCargadoresAsociados > 0)
                throw new AppValidationException($"Existen {cantidadUtilizacionCargadoresAsociados} utilizaciones de cargadores " +
                    $"asociados a {autobusExistente.Nombre_autobus}. No se puede eliminar");

            // Validamos que el autobus no tenga asociadas operaciones
            var cantidadOperacionesAutobusAsociados = await _autobusRepository
                .GetTotalAssociatedAutobusOperationAsync(autobusExistente.CodigoAutobus);

            if (cantidadOperacionesAutobusAsociados > 0)
                throw new AppValidationException($"Existen {cantidadOperacionesAutobusAsociados} operaciones de autobuses " +
                    $"asociados a {autobusExistente.Nombre_autobus}. No se puede eliminar");

            // Si existe y no tiene utilización de cargadores y operaciones asociadas, se puede eliminar
            try
            {
                bool resultadoAccion = await _autobusRepository
                    .DeleteAsync(autobusExistente);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");
            }
            catch (DbOperationException error)
            {
                throw error;
            }
        }
    }
}