using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shareds.Utilizing
{
    public class Hasher
    {
        public string JsonWebToken(string keystring, ICollection<KeyValuePair<string, string>> pairs)
        {
            var key = Encoding.ASCII.GetBytes(keystring);

            var claims = new List<Claim>();

            pairs?.ToList().ForEach(o => claims.Add(new Claim(o.Key, o.Value)));

            var descriptor = new SecurityTokenDescriptor();
            descriptor.Subject = new ClaimsIdentity(claims);
            descriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            
            var handle = new JwtSecurityTokenHandler();
            handle.SetDefaultTimesOnTokenCreation = false;

            var token = handle.CreateToken(descriptor);

            return handle.WriteToken(token);
        }


        public static async Task<string> Password(string username, string password)
        {
            string plain;

            try
            {
                var plaintext = new PlainText(username, password);

                plaintext.Plain = await Task.Run((() => { return Encoding.UTF8.GetBytes(plaintext.Text); } ));

                plain = await SHA512(plaintext);
            }
            catch
            {
                plain = string.Empty;
            }
            return plain;
        }

        public static async Task<bool> ComparePassword(string username, string password, string hash)
        {
            bool valid;
            try
            {
                valid = (await Password(username, password)).Equals(hash);
            }
            catch
            {
                valid = false;
            }
            return valid;
        }

        private static async Task<string> SHA512(PlainText plaintext)
        {
            string plain;

            try
            {
                byte[] salt = new byte[plaintext.Plain.Length + plaintext.SALT.Length];

                for (int i = 0; i < plaintext.Plain.Length; i++)
                    salt[i] = plaintext.Plain[i];
                
                for (int i = 0; i < plaintext.SALT.Length; i++)
                    salt[plaintext.Plain.Length + i] = plaintext.SALT[i];


                byte[] hash = await Task.Run((() => { return new SHA512Managed().ComputeHash(salt); }));

                salt = new byte[hash.Length + plaintext.SALT.Length];

                for (int i = 0; i < hash.Length; i++)
                    salt[i] = hash[i];
                
                for (int i = 0; i < plaintext.SALT.Length; i++)
                    salt[hash.Length + i] = plaintext.SALT[i];

                plain = await Task.Run((() => { return Convert.ToBase64String(hash); }));
            }
            catch
            {
                plain = string.Empty;
            }
            return plain;
        }

        internal class PlainText
        {
            public PlainText(string username, string password)
            {
                this.Text = string.Format("{0}{1}", username.ToLower().Trim(), password.Trim());
                this.SALT = Encoding.UTF8.GetBytes(password.Trim());
                this.Plain = Encoding.UTF8.GetBytes(password.Trim());
            }

            public string Text { get; set; }
            public byte[] SALT { get; set; }
            public byte[] Plain { get; set; }
        }
    }
}
