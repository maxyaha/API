
using Microservice.Application.Entities;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Microservice.Application.AppStart
{
    public class CustomAuthorize
    {
        private readonly string secret;
        private readonly string issuer;


        public CustomAuthorize(string secret, string issuer)
        {
            this.secret = secret;
            this.issuer = issuer;
        }

        public string GenerateJSONWebToken(JsonWebToken jwt)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("userID", jwt.UserID)
            };


            var token = new JwtSecurityToken(this.issuer,
              null,
              claims,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JsonWebToken DecodeJSONWebToken(string token)
        {
            JsonWebToken jwt = new JsonWebToken();

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.secret));
                var tokenToData = new JwtSecurityToken(jwtEncodedString: token);

                jwt.UserID = tokenToData.Claims.First(c => c.Type == "userID").Value;
           


                // ValidateToken
                SecurityToken validatedToken;
                TokenValidationParameters TVp = new TokenValidationParameters()
                {
                    ValidateIssuer = true,   // Because there is no issuer in the generated token
                    ValidateAudience = false, // Because there is no audiance in the generated token
                    ValidateLifetime = true, // Because there is no expiration in the generated token


                    ValidIssuer = this.issuer,
           
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.secret)) // The same key as the one that generate the token
                };
                IdentityModelEventSource.ShowPII = true;

                var tokenHandler = new JwtSecurityTokenHandler();


                IPrincipal principal = tokenHandler.ValidateToken(token, TVp, out validatedToken);
            }
            catch (Exception)
            {
                jwt = null;
            }
            return jwt;
        }
        public JsonWebToken DecodeJSONWebTokenFromCM(string token)
        {
            JsonWebToken jwt = new JsonWebToken();

            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.secret));

                // ValidateToken
                SecurityToken validatedToken;
                TokenValidationParameters TVp = new TokenValidationParameters()
                {
                    ValidateIssuer = false,   // Because there is no issuer in the generated token
                    ValidateAudience = false, // Because there is no audiance in the generated token
                    ValidateLifetime = false, // Because there is no expiration in the generated token


                    ValidIssuer = string.Empty,
           
                    IssuerSigningKey = securityKey // The same key as the one that generate the token
                };
                IdentityModelEventSource.ShowPII = true;

                var tokenHandler = new JwtSecurityTokenHandler();


                //IPrincipal principal 
                tokenHandler.ValidateToken(token, TVp, out validatedToken);
            }
            catch (Exception)
            {
                jwt = null;
            }
            return jwt;
        }


        public string GenerateTokenChangePassword(JsonWebToken jwt)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim("userID", jwt.UserID)
            };


            var token = new JwtSecurityToken(this.issuer,
              null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
