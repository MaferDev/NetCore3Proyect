using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta:IRequest<UsuarioData>
        {
            public string Nombre {get;set;}
            public string Apellidos {get;set;}
            public string Email {get;set;}
            public string Password {get;set;}
            public string Username {get;set;}
        }

        //Si la data es nula para que no permita
        public class EjecutaValidador: AbstractValidator<Ejecuta>
        {
            public EjecutaValidador(){
                RuleFor(x=>x.Nombre).NotEmpty();
                RuleFor(x=>x.Apellidos).NotEmpty();
                RuleFor(x=>x.Nombre).NotEmpty();
                RuleFor(x=>x.Email).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
                RuleFor(x=>x.Username).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta,UsuarioData>
        {
            private readonly CursosOnlineContext _context;
            private readonly  UserManager<Usuario> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Manejador( CursosOnlineContext context, UserManager<Usuario> userManager, IJwtGenerator jwtGenerator){
                _context=context;
                _userManager = userManager;
                _jwtGenerator=jwtGenerator;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var exist = await _context.Users.Where(x=>x.Email==request.Email).AnyAsync();

                if(exist)
                    throw(new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje="Existe un usuario registrado con este email."}));

                var existUsername = await _context.Users.Where(x=>x.UserName==request.Username).AnyAsync();

                if(existUsername)
                    throw(new ManejadorExcepcion(HttpStatusCode.BadRequest, new {mensaje="Existe un usuario registrado con este Username."}));


                var usuario = new Usuario{
                    NombreCompleto = request.Nombre + " " + request.Apellidos,
                    Email = request.Email,
                    UserName = request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if(resultado.Succeeded)
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerator.CrearToken(usuario),
                        Username=usuario.UserName,
                        Email=usuario.Email
                    };
                
                throw new ManejadorExcepcion(HttpStatusCode.InternalServerError);
            }
        }
    }
}