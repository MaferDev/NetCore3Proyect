using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Editar
    {
        public class Ejecuta:IRequest{
            public int CursoId {get;set;}
            public string Titulo {get;set;}
            public string Descripcion {get;set;}
            public DateTime? FechaPublicacion {get;set;}
        }

        //Se agrega esta clase para hacer las validaciones
        public class EjecutaValidacion:AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion(){
                //Se agrega la regla para la validación de los atributos
                RuleFor(x=>x.Titulo).NotEmpty();
                RuleFor(x=>x.Descripcion).NotEmpty();
                RuleFor(x=>x.FechaPublicacion).NotEmpty();
            }
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
                var curso = await _context.Curso.FindAsync(request.CursoId);
                if(curso==null){
                    throw new Exception("El curso no existe");
                }

                curso.Titulo = request.Titulo ?? curso.Titulo;
                curso.Descripcion = request.Descripcion ?? curso.Descripcion;
                curso.FechaPublicacion = request.FechaPublicacion ?? curso.FechaPublicacion;
                
                //0 = No se realizó la transacción - hubo errores 
                //1 = Se realizó la transacción
                //2 = 2 transacciones , etc...
                var valorTransaction = await _context.SaveChangesAsync();
                if(valorTransaction>0)
                    return Unit.Value;
                
                throw new Exception("No se pudo actualizar el curso");
            }
        }
    }
}