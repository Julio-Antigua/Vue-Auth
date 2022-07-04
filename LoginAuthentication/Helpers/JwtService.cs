using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginAuthentication.Helpers
{
    public class JwtService
    {
        private string securityKey = "this is a very secury key";
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            //Aqui obtenemos los byte de la securityKey

            var credentials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha256Signature);
            //Con esto creamos las credenciales 

            var header = new JwtHeader(credentials);
            var payLoad = new JwtPayload(id.ToString(),null,null,null,DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header,payLoad);//con esto creamos el token de seguridad

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(securityKey); //con esto optenemos los byte de la securityKey
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters 
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true ,
                ValidateIssuer = false,
                ValidateAudience = false
            },out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
