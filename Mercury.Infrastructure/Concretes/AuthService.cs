using Mercury.Infrastructure.Interfaces;
using Mercury.Infrastructure.Models;
using Mercury.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Mercury.Infrastructure.Concretes
{
    public class AuthService: IAuthService
    {
        private readonly AuthorizationSettings _authorizationSettings;

        public AuthService(IOptions<AuthorizationSettings> options)
        {
            _authorizationSettings = options.Value;
        }

        public string GenerateToken(long id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_authorizationSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Password CreatePasswordHash(string password)
        {
            if (password == null || string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException();
            }

            var encryptedPassword = new Password();

            using (var hmac = new HMACSHA512())
            {
                encryptedPassword.PasswordSalt = hmac.Key;
                encryptedPassword.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return encryptedPassword;
        }

        //public void VerifyPasswordHash(string password, Password encryptedPassword)
        //{
        //    if (password == null || string.IsNullOrWhiteSpace(password))
        //    {
        //        throw new InvalidOperationException();
        //    };

        //    using (var hmac = new HMACSHA512(encryptedPassword.PasswordSalt))
        //    {
        //        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        //        for (int i = 0; i < computedHash.Length; i++)
        //        {
        //            if (computedHash[i] != encryptedPassword.PasswordHash[i])
        //            {
        //                throw new InvalidOperationException();
        //            }
        //        }
        //    }
        //}

        public bool VerifyPasswordHash(string password, Password encryptedPassword)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException();
            };

            using (var hmac = new HMACSHA512(encryptedPassword.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != encryptedPassword.PasswordHash[i])
                    {
                        isValid = false;
                        break;
                    }
                }
            }

            return isValid;
        }
    }
}
