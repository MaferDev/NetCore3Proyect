using System.Linq;
using System.Security.Claims;
using Aplicacion.Contratos;
using Microsoft.AspNetCore.Http;

namespace Seguridad
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsuarioSesion(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string ObtenerUsuarioSesion()
        {
            //Sacar userName
            var userName = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x=>x.Type==ClaimTypes.NameIdentifier)?.Value;
            return userName;
        }
    }
}