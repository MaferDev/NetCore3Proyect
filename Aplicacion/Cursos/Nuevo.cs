using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Nuevo
    {
        public class Ejecuta:IRequest{
            public string Titulo {get;set;}
            public string Descripcion {get;set;}
            public DateTime? FechaPublicacion {get;set;}
            public List<Guid> ListaInstructor{get;set;}
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
                Guid _cursoId = Guid.NewGuid();
                var curso = new Curso{
                    CursoId=_cursoId,
                    Titulo=request.Titulo,
                    Descripcion=request.Descripcion,
                    FechaPublicacion=request.FechaPublicacion
                };
                _context.Curso.Add(curso);

                //insertamos la lista de instructores que tiene el curso
                if(request.ListaInstructor!=null){
                    foreach (var id in request.ListaInstructor)
                    {
                        var cursoInstructor = new CursoInstructor{
                            CursoId=_cursoId,
                            InstructorId=id
                        };
                        _context.CursoInstructor.Add(cursoInstructor);
                    }
                }

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