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

-- Creamos las colecciones usando un json schema para validación
db.createCollection("cargadores", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Cargadores de Buses del Transporte Público Eléctrico",
         required: ["nombre_cargador"],
         properties: {
            nombre_cargador: {
               bsonType: "string",
               description: "'nombre_cargador' Debe ser una cadena de caracteres y no puede ser nulo"
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
         properties: {
            nombre_autobus: {
               bsonType: "string",
               description: "'nombre_autobus' Debe ser una cadena de caracteres y no puede ser nulo"
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
         required: ["id", "horario_pico"],
         properties: {
            id: {
               bsonType: "int",
               description: "'id' Debe ser un número entero y no puede ser nulo"
            },
            horario_pico: {
               description: "'horario_pico' Debe ser un valor booleano (true o false) y no puede ser nulo"
            }
         }
      }
   }
});

db.createCollection("utilizacion_cargadores", {
   validator: {
      $jsonSchema: {
         bsonType: "object",
         title: "Utilización de Cargadores de Buses del Transporte Público Eléctrico",
         required: ["cargador_id", "autobus_id", "horario_id"],
         properties: {
            cargador_id: {
               bsonType: "int",
               description: "'cargador_id' Debe ser un número entero y no puede ser nulo"
            },
            autobus_id: {
               bsonType: "int",
               description: "'autobus_id' Debe ser un número entero y no puede ser nulo"
            },
            horario_id: {
               bsonType: "int",
               description: "'horario_id' Debe ser un número entero y no puede ser nulo"
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
         required: ["autobus_id", "horario_id"],
         properties: {
            autobus_id: {
               bsonType: "int",
               description: "'autobus_id' Debe ser un número entero y no puede ser nulo"
            },
            horario_id: {
               bsonType: "int",
               description: "'horario_id' Debe ser un número entero y no puede ser nulo"
            }
         }
      }
   }
});

-- ***************************************************
-- Consultas de apoyo para implementar el repositorio
-- ***************************************************

