using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class CargadorService
    {
        private readonly ICargadorRepository _cargadorRepository;

        public CargadorService(ICargadorRepository cargadorRepository)
        {
            _cargadorRepository = cargadorRepository;
        }

        public async Task<IEnumerable<Cargador>> GetAllAsync()
        {
            return await _cargadorRepository
                .GetAllAsync();
        }

        public async Task<Cargador> GetByIdAsync(int codigo_cargador)
        {
            // Validamos que el cargador exista con ese Id
            var unCargador = await _cargadorRepository
                .GetByIdAsync(codigo_cargador);

            if (unCargador.Codigo_cargador == 0)
                throw new AppValidationException($"Cargador no encontrado con el id {codigo_cargador}");

            return unCargador;
        }

        public async Task<Cargador> CreateAsync(Cargador unCargador)
        {
            // Validamos que el nombre no exista previamente
            var cargadorExistente = await _cargadorRepository
                .GetByNameAsync(unCargador.Nombre_cargador);

            if (cargadorExistente.Codigo_cargador != 0)
                throw new AppValidationException($"Ya existe un cargador con el nombre {cargadorExistente.Nombre_cargador}");
            try
            {
                bool resultadoAccion = await _cargadorRepository
                    .CreateAsync(unCargador);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                unCargador = await _cargadorRepository
                    .GetByNameAsync(unCargador.Nombre_cargador!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return cargadorExistente;
        }

        public async Task<Cargador> UpdateAsync(int codigo_cargador, Cargador unCargador)
        {
            // Validamos que los parómetros sean consistentes
            if (codigo_cargador != unCargador.Codigo_cargador)
                throw new AppValidationException($"Inconsistencia en el Id del cargador a actualizar. Verifica argumentos");

            // Validamos que el cargador exista con ese Id
            var cargadorExistente = await _cargadorRepository
                .GetByIdAsync(codigo_cargador);

            if (cargadorExistente.Codigo_cargador == 0)
                throw new AppValidationException($"No existe un cargador registrado con el id {unCargador.Codigo_cargador}");

            // Validamos que el cargador tenga nombre
            if (unCargador.Nombre_cargador.Length == 0)
                throw new AppValidationException("No se puede actualizar un cargador con nombre nulo");

            // Validamos que el nombre no exista previamente en otro cargador diferente al que se estó actualizando
            cargadorExistente = await _cargadorRepository
                .GetByNameAsync(unCargador.Nombre_cargador);

            if (unCargador.Nombre_cargador == cargadorExistente.Nombre_cargador)
                throw new AppValidationException($"Ya existe otro autobus con el nombre {unCargador.Nombre_cargador}. " +
                    $"No se puede Actualizar");

            // Validamos que haya al menos un cambio en las propiedades
            if (unCargador.Equals(cargadorExistente))
                throw new AppValidationException("No hay cambios en los atributos del cargador. No se realiza actualización.");

            try
            {
                bool resultadoAccion = await _cargadorRepository
                    .UpdateAsync(unCargador);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                cargadorExistente = await _cargadorRepository
                    .GetByNameAsync(unCargador.Nombre_cargador!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return cargadorExistente;
        }

        public async Task DeleteAsync(int codigo_cargador)
        {
            // Validamos que el cargador a eliminar si exista con ese Id
            var cargadorExistente = await _cargadorRepository
                .GetByIdAsync(codigo_cargador);

            if (cargadorExistente.Codigo_cargador == 0)
                throw new AppValidationException($"No existe un cargador con el Id {codigo_cargador} que se pueda eliminar");

            // Validamos que el cargador no tenga asociadas utilizaciones
            var cantidadUtilizacionesAsociados = await _cargadorRepository
                .GetTotalAssociatedChargerUtilizationAsync(cargadorExistente.Codigo_cargador);

            if (cantidadUtilizacionesAsociados > 0)
                throw new AppValidationException($"Existen {cantidadUtilizacionesAsociados} cargadores " +
                    $"asociados a {cargadorExistente.Nombre_cargador}. No se puede eliminar");

            // Si existe y no tiene utilizaciones asociadas, se puede eliminar
            try
            {
                bool resultadoAccion = await _cargadorRepository
                    .DeleteAsync(cargadorExistente);

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