using Mercury.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mercury.Infrastructure.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(long id);
        Password CreatePasswordHash(string password);
        //void VerifyPasswordHash(string password, Password encryptedPassword);
        bool VerifyPasswordHash(string password, Password encryptedPassword);
    }
}
