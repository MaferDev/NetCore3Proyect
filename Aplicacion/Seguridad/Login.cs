using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta:IRequest<UsuarioData>
        {
            public string Email {get;set;}    
            public string Password {get;set;}    
        }

        //Validación con fluentValidation
        public class EjecutaValidacion:AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x=>x.Email).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerator jwtGenerator)
            {
                _signInManager=signInManager;
                _userManager=userManager;
                _jwtGenerator=jwtGenerator;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Verificar que el email exista
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if(usuario==null){
                    throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
                }

                var resultado = await _signInManager.CheckPasswordSignInAsync(usuario,request.Password,false);
                if(resultado.Succeeded){
                    return new UsuarioData {
                        NombreCompleto=usuario.NombreCompleto,
                        Token=_jwtGenerator.CrearToken(usuario),
                        Email=usuario.Email,
                        Username=usuario.UserName,
                        Imagen=null
                    };
                }
                
                throw new ManejadorExcepcion(HttpStatusCode.Unauthorized);
            }
        }
    }
}