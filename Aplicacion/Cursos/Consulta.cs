using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Cursos
{
    public class Consulta
    {
        public class ListaCursos:IRequest<List<CursoDto>>{            
        }

        public class Manejador : IRequestHandler<ListaCursos, List<CursoDto>>
        {
            private readonly CursosOnlineContext _context;
            public Manejador(CursosOnlineContext context){
                _context=context;
            }
            public async Task<List<CursoDto>> Handle(ListaCursos request, CancellationToken cancellationToken)
            {
                //Agregamos para que traiga el id del instructor y los datos del instructor
                var cursos = await _context.Curso
                    .Include(x=>x.InstructoresLink)
                    .ThenInclude(x=>x.Instructor)
                    .ToListAsync();
                //Herramienta para mapear clases  


                return cursos;
            }
        }
    }
}