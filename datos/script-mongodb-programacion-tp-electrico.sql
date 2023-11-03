-- Script MongoDB
-- Curso de Tópicos Avanzados de base de datos - UPB 202320
-- Samuel Pérez Hurtado ID 000459067 - Luisa María Flórez Múnera ID 000449529

-- Proyecto: Gestión de Programación de Transporte Público Eléctrico
-- Motor de Base de datos: MongoDB - 7.0.2

-- ***********************************
-- Abastecimiento de imagen en Docker
-- ***********************************
 
-- Descargar la imagen -- https://hub.docker.com/_/mongo
docker pull mongo:latest

-- Crear el contenedor
docker run --name mongodb-programacion-tp-electrico -e MONGO_INITDB_ROOT_USERNAME=mongoadmin -e MONGO_INITDB_ROOT_PASSWORD=programacion_transporte -p 27017:27017 -d mongo:latest

-- ****************************************
-- Creación de base de datos y usuarios
-- ****************************************

-- Con usuario mongoadmin:
-- Crear la base de datos
use programacion_tp_electrico_db;

-- Crear el usuario con privilegios limitados
db.createUser({
  user: "programacion_tp_electrico_usr",
  pwd: "programacion_transporte",
  roles: [
    {
      role: "readWrite",
      db: "programacion_tp_electrico_db"
    }
  ]
});

-- Creamos las colecciones sin validación
db.createCollection("cargadores");
db.createCollection("autobuses");
db.createCollection("horarios");
db.createCollection("utilizacion_cargadores");
db.createCollection("operacion_autobuses");

db.createCollection("cargadores", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Cargadores de Buses del Transporte Público Eléctrico",
         required: ["nombre_cargador"],  
         properties: {
            nombre_cargador: {
               bsonType: "string",
               description: "'nombre_cargador' debe ser una cadena de caracteres y no puede ser nulo"
            }
         }
      }
   }
});


db.createCollection("autobuses", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Autobuses del Transporte Público Eléctrico",
         required: ["nombre_autobus"],  
         properties: 
            nombre_autobus: {
               bsonType: "string",
               description: "'nombre_autobus' debe ser una cadena de caracteres y no puede ser nulo"
            }
         }
      }
   }
});

db.createCollection("horarios", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Horarios de Operación del Transporte Público Eléctrico",
         required: ["hora", "horario_pico"],  // Incluye el nuevo campo "hora" antes de "horario_pico"
         properties: {
            hora: {
               bsonType: "int",  // Define el tipo como entero int para "hora"
               description: "'hora' debe ser un número entero y no puede ser nulo"
            },
            horario_pico: {
               description: "'horario_pico' debe ser un valor booleano (true o false) y no puede ser nulo"
            }
         },
         propertyNames: {
            description: "Orden de campos: hora, id, horario_pico"
         }
      }
   }
});

db.createCollection("utilizacion_cargadores", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Utilización de Cargadores de Buses del Transporte Público Eléctrico",
         required: ["cargador_id", "autobus_id", "hora"],  
         properties: {
            cargador_id: {
               bsonType: "string",
               description: "'cargador_id' debe ser un número entero y no puede ser nulo"
            },
            autobus_id: {
               bsonType: "string",
               description: "'autobus_id' debe ser un número entero y no puede ser nulo"
            },
            hora: {
               bsonType: "int",
               description: "'hora' debe ser un número entero y no puede ser nulo"
            }
         }
      }
   }
});


db.createCollection("operacion_autobuses", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Operación de Autobuses del Transporte Público Eléctrico",
         required: ["codigo_autobus", "hora"],
         properties: {
            codigo_autobus: {
               bsonType: "int",
               description: "'codigo_autobus' Debe ser un número entero y no puede ser nulo"
            },
            hora: {
               bsonType: "int",
               description: "'hora' Debe ser un número entero y no puede ser nulo"
            }
         }
      }
   }
});

-- ***************************************************
-- Consultas de apoyo para implementar el repositorio
-- ***************************************************

--Todos los autobuses
db.autobuses.find({});

--Todos los autobuses por id
db.autobuses.find({codigo_autobus: 1});

--Todos los autobuses por nombre
db.autobuses.find({nombre_autobus: "Circular"});

--Todos los cargadores
db.cargadores.find({});

--Todos los cargadores por id
db.cargadores.find({codigo_cargador: 1});

--Todos los cargadores por nombre
db.cargadores.find({nombre_cargador: "Cargador 1"});

--Todos los horarios
db.horarios.find({});

--Todos los horarios por id
db.horarios.find({hora: 1});

--Todos los horarios por horario_pico
db.horarios.find({horario_pico: true});

--Todos las operaciones de los autobuses
db.operacion_autobuses.find({});

--Todos las operaciones de los autobuses por id
db.operacion_autobuses.find({codigo_autobus: 1});

--Todos las operaciones de los autobuses por hora
db.operacion_autobuses.find({hora: 1});

--Todos las utilizaciones de los cargadores
db.utilizacion_cargadores.find({});

--Todos las utilizaciones de los cargadores por id
db.utilizacion_cargadores.find({codigo_cargador: 1});

--Todos las utilizaciones de los cargadores por hora
db.utilizacion_cargadores.find({hora: 1});

--Todos las utilizaciones de los cargadores por autobus
db.utilizacion_cargadores.find({codigo_autobus: 1});




