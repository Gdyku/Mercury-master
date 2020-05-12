using Mercury.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Intefaces
{
    public interface IAuthLogic
    {
        Task<UserDto> SignUpPlayer(SignUpDto credentials);
        Task<UserDto> SignInUser(UserCredentialsDto credentials);
        Task<UserDto> GetUserByEmail(string email);
        Task<bool> ValidateUser(long id, string password);
    }
}
