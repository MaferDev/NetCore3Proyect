using System.Collections.Generic;
using System.Security.Claims;
using Aplicacion.Contratos;
using System.IdentityModel.Tokens.Jwt;
using Dominio;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace Seguridad
{
    public class JwtGenerator : IJwtGenerator
    {
        public string CrearToken(Usuario usuario)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.NameId,usuario.UserName)
            };

            //Credenciales de acceso
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Mi palabra secreta"));
            var Credenciales = new SigningCredentials(key,SecurityAlgorithms.HmacSha256Signature);
            //Descripción del token
            var tokenDescripcion = new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(30),
                SigningCredentials=Credenciales
            };

            //Crear el token

            var tokenManejador = new JwtSecurityTokenHandler();
            var token = tokenManejador.CreateToken(tokenDescripcion);

            //Token en string
            return tokenManejador.WriteToken(token);

        }
    }
}