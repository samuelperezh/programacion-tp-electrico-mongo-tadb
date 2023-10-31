﻿/*
 AppValidationException:
 Excepcion creada para enviar mensajes relacionados 
 con la validación en todas las operaciones CRUD de la aplicación
*/

namespace ProgramacionTP_CS_API_Mongo.Helpers
{
    public class AppValidationException : Exception
    {
        public AppValidationException(string message) : base(message) { }
    }
}