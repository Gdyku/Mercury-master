using Mercury.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mercury.BLL.Intefaces
{
    public interface IUserLogic
    {
        Task<List<UserDto>> GetUsers(int itemPerPage, int page);
        Task<UserDto> GetUser(long userId);
        Task<UserDto> CreateUser(NewUserDto user);
        Task<UserDto> UpdateUser(UserDto user);
        Task DeleteUser(long userId);
        Task<IEnumerable<UserDto>> GetSubscribingUsers();
    }
}
