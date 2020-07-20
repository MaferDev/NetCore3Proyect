
using Microsoft.AspNetCore.Identity;

namespace Dominio
{
    //Identity use contiene todas las propiedades que coreIdentity va manejar
    //Como email, passwork, etc
    public class Usuario :IdentityUser
    {
        public string NombreCompleto {get;set;}
    }
}