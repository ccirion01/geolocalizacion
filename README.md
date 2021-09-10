Solución para geocodificar direcciones implementando una arquitectura basada en microservicios. 

Consta de un servicio 1 APIGeo que recibe una dirección a geolocalizar y la persiste en base de datos, y se comunica con el servicio 2 Geocodificador que realiza la geocodificación propiamente. 
Una vez hecho esto, el servicio 2 se comunica nuevamente con el servicio 1 y éste actualiza la base de datos con el resultado de la geocodificación.

Tecnologías utilizadas:
.Net core 3.1
SQL Server
EF
RabbitMQ


Configuración de entorno:
-Levantar el contenedor de Docker de RabbitMQ mediante el comando:

docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management


- Creación de base de datos en SQL Server. Script:

CREATE DATABASE APIGeo;
CREATE TABLE Direcciones (
    Id uniqueidentifier NOT NULL,
    Estado int NOT NULL,
    Calle nvarchar(max) NOT NULL,
	Numero int NOT NULL,
	Ciudad nvarchar(max) NOT NULL,
    CodigoPostal int NOT NULL,
    Provincia nvarchar(max) NOT NULL,
	Pais nvarchar(max) NOT NULL,
	Latitud float,
	Longitud float,
	PRIMARY KEY (Id)
);

- Agregar parametrización de connectionString en archivo appSettings.json del servicio APIGeo:

"ConnectionStrings": {
    "DireccionDbContext": --connection string--
  }
  

Se omite por el momento la contenerización de los servicios en contenedores Docker por falta de tiempo para investigación.
