using System.Collections.Generic;
using System.Threading.Tasks;
using Aplicacion.Cursos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    //http://localhost:500/api/Cursos
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController:MyControllerBase
    {
        // Ya viene de Controller Base
        /*
        private readonly IMediator _mediator;
        public CursosController(IMediator mediator){
            _mediator=mediator;
        }
        */

        [HttpGet]
        public async Task<ActionResult<List<Curso>>> Get(){
            return await Mediator.Send(new Consulta.ListaCursos());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetById(int id){
            return await Mediator.Send(new ConsultaId.CursoUnico{Id=id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data){
            return await Mediator.Send(data);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id,Editar.Ejecuta data){
            data.CursoId = id;

            return await Mediator.Send(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Eliminar(int id){
            return await Mediator.Send(new Eliminar.Ejecuta{Id=id});
        }
    }
}