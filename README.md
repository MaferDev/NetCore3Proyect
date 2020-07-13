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

La capa aplicación es la encargada de manejar la logica de negocio.

~~~
~~~
~~~
~~~
~~~
~~~

