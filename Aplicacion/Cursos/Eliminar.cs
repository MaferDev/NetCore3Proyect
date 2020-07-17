using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistencia;
using Aplicacion.ManejadorError;
using System.Net;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta:IRequest{
            public int Id {get;set;}
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
                var curso = await _context.Curso.FindAsync(request.Id);
                if(curso==null){
                    //throw new Exception("No se encontro curso para eliminar");
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound,new {mensaje="No se encontro el curso"}); 
                }
                //0 = No se realizó la transacción - hubo errores 
                //1 = Se realizó la transacción
                //2 = 2 transacciones , etc...
                _context.Remove(curso);
                var valorTransaction = await _context.SaveChangesAsync();
                if(valorTransaction>0)
                    return Unit.Value;
                
                throw new Exception("No se pudo eliminar el curso");
            }
        }
    }
}