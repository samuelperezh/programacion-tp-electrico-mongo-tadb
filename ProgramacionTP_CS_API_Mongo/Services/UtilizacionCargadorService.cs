using ProgramacionTP_CS_API_Mongo.Helpers;
using ProgramacionTP_CS_API_Mongo.Interfaces;
using ProgramacionTP_CS_API_Mongo.Models;
using ProgramacionTP_CS_API_Mongo.Repositories;

namespace ProgramacionTP_CS_API_Mongo.Services
{
    public class UtilizacionCargadorService
    {
        private readonly IUtilizacionCargadorRepository _utilizacionCargadorRepository;
        private readonly ICargadorRepository _cargadorRepository;
        private readonly IAutobusRepository _autobusRepository;
        private readonly IHorarioRepository _horarioRepository;
        private readonly IOperacionAutobusRepository _operacionAutobusRepository;


        public UtilizacionCargadorService(IUtilizacionCargadorRepository utilizacionCargadorRepository,
                                          ICargadorRepository cargadorRepository,
                                          IAutobusRepository autobusRepository,
                                          IHorarioRepository horarioRepository,
                                          IOperacionAutobusRepository operacionAutobusRepository)
        {
            _utilizacionCargadorRepository = utilizacionCargadorRepository;
            _cargadorRepository = cargadorRepository;
            _autobusRepository = autobusRepository;
            _horarioRepository = horarioRepository;
            _operacionAutobusRepository = operacionAutobusRepository;
        }

        public async Task<IEnumerable<UtilizacionCargador>> GetAllAsync()
        {
            return await _utilizacionCargadorRepository
                .GetAllAsync();
        }
        
        public async Task<UtilizacionCargador> CreateAsync(UtilizacionCargador unaUtilizacionCargador)
        {
            //Validamos que el cargador exista con ese Id
            if (string.IsNullOrEmpty(unaUtilizacionCargador.Cargador_id))
                throw new AppValidationException($"El id del cargador no puede ser nulo");

            var cargadorExistente = await _cargadorRepository
                .GetByIdAsync(unaUtilizacionCargador.Cargador_id);

            if (string.IsNullOrEmpty(cargadorExistente.Id))
                throw new AppValidationException($"El cargador con id {cargadorExistente.Id} no se encuentra registrado");

            //Validamos que el autobus exista con ese Id
            if (string.IsNullOrEmpty(unaUtilizacionCargador.Autobus_id))
                throw new AppValidationException($"El id del autobus no puede ser nulo");

            var autobusExistente = await _autobusRepository
                .GetByIdAsync(unaUtilizacionCargador.Autobus_id);

            if (string.IsNullOrEmpty(autobusExistente.Id))
                throw new AppValidationException($"El autobus con id {autobusExistente.Id} no se encuentra registrado");

            //Validamos que el horario exista con ese Id
            var horarioExistente = await _horarioRepository
                .GetByIdAsync(unaUtilizacionCargador.Hora);

            if (horarioExistente.Hora < 0 || horarioExistente.Hora > 23)
                throw new AppValidationException($"El horario {horarioExistente.Hora} no se encuentra registrado");

            //Validamos que se pueda hacer la utilización por dos horas consecutivas
            string estado_hora = await _operacionAutobusRepository.GetAutobusStateAsync(unaUtilizacionCargador.Hora, unaUtilizacionCargador.Autobus_id);
            string estado_hora_siguiente = await _operacionAutobusRepository.GetAutobusStateAsync(unaUtilizacionCargador.Hora + 1, unaUtilizacionCargador.Autobus_id);

            if (estado_hora == "Cargando" || estado_hora_siguiente == "Cargando") {
                throw new AppValidationException($"Ya existe una utilización de cargador en el autobus {unaUtilizacionCargador.Autobus_id}, en el cargador {unaUtilizacionCargador.Cargador_id}, en las horas {unaUtilizacionCargador.Hora} y {unaUtilizacionCargador.Hora + 1}");
            }

            else if (estado_hora == "Operando" || estado_hora_siguiente == "Operando") {
                throw new AppValidationException($"El autobus con id {unaUtilizacionCargador.Autobus_id}, no se puede poner a cargar en las horas {unaUtilizacionCargador.Hora} y {unaUtilizacionCargador.Hora + 1} porque está operando");
            }

            //Validamos que la utilización no exista previamente
            var utilizacionCargadorExistente = await _utilizacionCargadorRepository
                .GetByUtilizationAsync(unaUtilizacionCargador.Cargador_id, unaUtilizacionCargador.Autobus_id, unaUtilizacionCargador.Hora);

            if (string.IsNullOrEmpty(utilizacionCargadorExistente.Cargador_id) == false)
                throw new AppValidationException($"Ya existe una utilización de cargador en el autobus {utilizacionCargadorExistente.Autobus_id}, en el cargador {utilizacionCargadorExistente.Cargador_id}, en la horas {utilizacionCargadorExistente.Hora} y {utilizacionCargadorExistente.Hora + 1}");

            try
            {
                bool resultadoAccion = await _utilizacionCargadorRepository
                    .CreateAsync(unaUtilizacionCargador);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                unaUtilizacionCargador = await _utilizacionCargadorRepository
                    .GetByUtilizationAsync(unaUtilizacionCargador.Cargador_id!, unaUtilizacionCargador.Autobus_id!, unaUtilizacionCargador.Hora!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return utilizacionCargadorExistente;
        }

        public async Task<UtilizacionCargador> UpdateAsync(string cargador_id, string autobus_id, int hora, UtilizacionCargador unaUtilizacionCargador)
        {
            //Validamos que el cargador exista con ese Id
            var cargadorExistente = await _cargadorRepository
                .GetByIdAsync(cargador_id);

            if (string.IsNullOrEmpty(cargadorExistente.Id))
                throw new AppValidationException($"El cargador con id {cargadorExistente.Id} no se encuentra registrado");

            // Validamos que el autobus exista con ese Id
            var autobusExistente = await _autobusRepository
                .GetByIdAsync(autobus_id);

            if (string.IsNullOrEmpty(autobusExistente.Id))
                throw new AppValidationException($"El autobus con id {autobusExistente.Id} no se encuentra registrado");

            //Validamos que el horario exista con ese Id
            var horarioExistente = await _horarioRepository
                .GetByIdAsync(hora);

            if (horarioExistente.Hora < 0 || horarioExistente.Hora > 23)
                throw new AppValidationException($"El horario {horarioExistente.Hora} no se encuentra registrado");

            //Validamos que la utilización exista previamente
            var utilizacionCargadorExistente = await _utilizacionCargadorRepository
                .GetByUtilizationAsync(unaUtilizacionCargador.Cargador_id, unaUtilizacionCargador.Autobus_id, unaUtilizacionCargador.Hora);

            if (string.IsNullOrEmpty(utilizacionCargadorExistente.Cargador_id))
                throw new AppValidationException($"No existe una utilización de cargador en el autobus {utilizacionCargadorExistente.Autobus_id}, en el cargador {utilizacionCargadorExistente.Cargador_id}, en la hora {utilizacionCargadorExistente.Hora}");

            //Validamos que haya al menos un cambio en las propiedades
            if (unaUtilizacionCargador.Equals(utilizacionCargadorExistente))
                throw new AppValidationException("No hay cambios en los atributos de la utilización cargador. No se realiza actualización.");

            try
            {
                bool resultadoAccion = await _utilizacionCargadorRepository
                    .UpdateAsync(unaUtilizacionCargador);

                if (!resultadoAccion)
                    throw new AppValidationException("Operación ejecutada pero no generó cambios en la DB");

                utilizacionCargadorExistente = await _utilizacionCargadorRepository
                    .GetByUtilizationAsync(unaUtilizacionCargador.Cargador_id!, unaUtilizacionCargador.Autobus_id!, unaUtilizacionCargador.Hora!);
            }
            catch (DbOperationException error)
            {
                throw error;
            }

            return utilizacionCargadorExistente;
        }

        public async Task DeleteAsync(int cargador_id, int autobus_id, int hora)
        {
            // Validamos que la utilización del cargador a eliminar si exista previamente
            var utilizacionCargadorExistente = await _utilizacionCargadorRepository
                .GetByUtilizationAsync(cargador_id, autobus_id, hora);

            if (utilizacionCargadorExistente.Cargador_id == 0)
                throw new AppValidationException($"No existe una utilización de cargador en el autobus {utilizacionCargadorExistente.Autobus_id}, en el cargador {utilizacionCargadorExistente.Cargador_id}, en la hora {utilizacionCargadorExistente.Hora}");

            //Si existe se puede eliminar
            try
            {
                bool resultadoAccion = await _utilizacionCargadorRepository
                    .DeleteAsync(utilizacionCargadorExistente);

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