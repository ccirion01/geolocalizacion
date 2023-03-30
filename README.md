## POC - Geolocation
Solution for geocoding addresses implementing a microservices-based architecture.

It consists of an **APIGeo** service that receives an address to geolocate and persists it in a database, and then communicates it to a **Geocoder** service which performs the geocoding itself.  <br>
Once this service has completed the geocoding process, it sends the results back to the **APIGeo** service, which in turn updates its database with the resulting geocoded information.

### Tech stack
- .Net core 3.1
- SQL Server
- Entity Framework
- RabbitMQ

### Environment Setup
- Start the Docker container of RabbitMQ using the following command:

`docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management`

- Create a database in SQL Server using the following script:

`CREATE DATABASE APIGeo;`
<br>
`CREATE TABLE Direcciones (`
<br>
&nbsp;&nbsp;&nbsp; `Id uniqueidentifier NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Estado int NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Calle nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Numero int NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Ciudad nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `CodigoPostal int NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Provincia nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Pais nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Latitud float,` <br>
&nbsp;&nbsp;&nbsp; `Longitud float,` <br>
&nbsp;&nbsp;&nbsp; `PRIMARY KEY (Id));` <br>

- Add the connection string of the database in the *appSettings.json* file of the **APIGeo** service:

`"ConnectionStrings": {
    "DireccionDbContext": --connection string--
  }`
  
### Challenge
Implement containerization of the services using Docker.

<hr/>

## POC - Geolocalización
Solución para geocodificar direcciones implementando una arquitectura basada en microservicios. 

Consta de un servicio **APIGeo** que recibe una dirección a geolocalizar y la persiste en base de datos, y se comunica con el servicio **Geocodificador** que realiza la geocodificación propiamente. <br>
Una vez hecho esto, el servicio **Geocodificador** se comunica nuevamente con el servicio **APIGeo** y éste actualiza su base de datos con el resultado de la geocodificación.

### Tecnologías utilizadas
- .Net core 3.1
- SQL Server
- Entity Framework
- RabbitMQ

### Configuración de entorno
- Levantar el contenedor de Docker de RabbitMQ mediante el comando:

`docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.9-management`

- Crear base de datos en SQL Server mediante el siguiente Script:

`CREATE DATABASE APIGeo;`
<br>
`CREATE TABLE Direcciones (`
<br>
&nbsp;&nbsp;&nbsp; `Id uniqueidentifier NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Estado int NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Calle nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Numero int NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Ciudad nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `CodigoPostal int NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Provincia nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Pais nvarchar(max) NOT NULL,` <br>
&nbsp;&nbsp;&nbsp; `Latitud float,` <br>
&nbsp;&nbsp;&nbsp; `Longitud float,` <br>
&nbsp;&nbsp;&nbsp; `PRIMARY KEY (Id));` <br>

- Agregar parametrización del connectionString de la base de datos en el archivo *appSettings.json* del servicio APIGeo:

`"ConnectionStrings": {
    "DireccionDbContext": --connection string--
  }`
  
### Desafío
Implementar la contenerización de los servicios utilizando Docker.
