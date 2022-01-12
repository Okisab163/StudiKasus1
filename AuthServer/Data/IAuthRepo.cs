using AuthServer.Dtos;
using AuthServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Data
{
    public interface IAuthRepo
    {
        //Role
        IEnumerable<RoleForCreateDto> GetAllRoles();
        Task AddRole(string roleName);
        Task AddUserToRole(string username, string role);

        //User
        IEnumerable<UserDto> GetAllUsers();
        Task<List<string>> GetRolesFromUser(string username);

        //Administration
        Task<User> Authentication(string username, string password);
        Task Registration(UserForCreateDto user);
    }
}
