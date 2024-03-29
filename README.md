# Estructura de una solucion ASP.NET Core MultiCapas
## Creación del proyecto
### Para crear la solucion del proyecto:
~~~
dotnet new sln ProyectoNetCore
~~~

### Para crear los proyectos de clase:
~~~
dotnet new classlib -o Dominio
dotnet new classlib -o Persistencia
dotnet new classlib -o Aplicacion
~~~

### Para crear el proyecto API:
~~~
dotnet new webapi -o WebAPI
~~~

## Enlazar los proyectos a la Solución:
~~~
dotnet sln add Dominio/
dotnet sln add Persistencia/
dotnet sln add Aplicacion/
dotnet sln add WebAPI/
~~~

## Creación de dependencias
Ingresamos a la carpeta de Aplicacion y agregamos la referencia a Dominio
~~~
dotnet add reference ../Dominio/
dotnet add reference ../Persistencia/
~~~

Ingresamos a la carpeta de WebAPI y agregamos la referencia a Aplicacion
~~~
dotnet add reference ../Aplicacion/
~~~

Ingresamos a la carpeta de Persistencia y agregamos la referencia a Dominio
~~~
dotnet add reference ../Dominio/
~~~

## Crear Controler en WebAPI Core

En objetivo de las clases controllers es de recibir y manejar requerimientos, requests de  clientes, mas no manejar logica de negocios.

### Desarrollo de la capa Aplicación
incorporamos librerias al proyecto aplicacion.
MediatR.Extensions

La capa de aplicación es el encargado del manejo de la logica del negocio.

CancellationToken => cancela la petición asincrona en el backend una vez solicitada.

## Validaciones con Fluente

Instalamos con package Manager FluentValidation.AspNetCore

##Mensajes del Servidor

2xx = Transacción Correcta
3xx = No se modifico
4xx = Errores en el frontend 
5xx = Errores en el servidor (Backend)

### Middleware 
Los middleware son necesario para mostrar un mensaje al usuario cuando el proyecto este publicado en producción.

# Migración con Entity Framework Core y Sql Server
## Creación de Archivos de Migración

Agraremos un packete [`Microsoft.AspNetCore.Identity.EntityFrameworkCore`]

Instalaremos dotnet tools para la migración
[`dotnet tool install --global dotnet-ef --version 3.1.1`]

Creamos el archivo para la migración que se va guardar en Persistencia y se va ejecutar en WebApi
[`dotnet ef migrations add IdentityCoreInicial -p Persistencia/ -s WebAPI/`]

## Configurar WebAPI para ejecutar el archivo de migración
Se modifica el archivo programs de la capa WebAPI, y se ejecuta el comando dentro del proyecto.
[`cd WebAPI`]
[`dotnet watch run`]

# Tokens JWT Json Web Token
Se trabajara en una nueva capa Seguridad
[`dotnet new classlib -o Seguridad`]

Agregamos a la solución
[` dotnet sln add Seguridad/`]

Agregamos la referencia de seguridad a Aplicacion
[` cd Seguridad `]
[` dotnet add reference ../Aplicacion `]

Referenciamos de WebAPI hacia Seguridad
[` cd WebAPI `]
[` dotnet add reference ../Seguridad `]

## Crear Token
Se instalara una dependencia
System.IdentityModel.Token_Jwt

Para agregar seguridad a los controladores vamos a utilizar JwtBearerDefaults
Instalamos por Nuget
Microsoft.AspNetCore.Authentication.JwtBearer

# DTO 
Un DTO es una clase especial que esta orientada a entregar data especifica a un cliente.
~~~
~~~
~~~
~~~
~~~
~~~

