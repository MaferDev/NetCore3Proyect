using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta:IRequest{
            public int CursoId {get;set;}
            public string Titulo {get;set;}
            public string Descripcion {get;set;}
            public DateTime FechaPublicacion {get;set;}
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context)
            {
                _context=context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = new Curso{
                    Titulo=request.Titulo,
                    Descripcion=request.Descripcion,
                    FechaPublicacion=request.FechaPublicacion
                };
                _context.Curso.Add(curso);

                //0 = No se realizó la transacción - hubo errores 
                //1 = Se realizó la transacción
                //2 = 2 transacciones , etc...
                var valorTransaction = await _context.SaveChangesAsync();
                if(valorTransaction>0)
                    return Unit.Value;
                
                throw new Exception("No se pudo insertar el curso");
            }
        }
    }
}