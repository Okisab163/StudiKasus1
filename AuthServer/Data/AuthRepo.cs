using AuthServer.Dtos;
using AuthServer.Helpers;
using AuthServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Data
{
    public class AuthRepo : IAuthRepo
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppSettings _appSettings;

        public AuthRepo(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSetings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _appSettings = appSetings.Value;
        }

        public async Task AddRole(string roleName)
        {
            IdentityResult roleResult;
            try
            {
                var roleIsExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleIsExist)
                {
                    roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                else
                {
                    throw new Exception($"Role {roleName} Sudah Ada");
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task AddUserToRole(string username, string role)
        {
            var user = await _userManager.FindByNameAsync(username);
            try
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<User> Authentication(string username, string password)
        {
            try
            {
                var userFind = await _userManager.CheckPasswordAsync(await _userManager.FindByNameAsync(username), password);
                if (!userFind)
                {
                    return null;
                }

                var user = new User
                {
                    Username = username
                };

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Username));
                var roles = await GetRolesFromUser(username);
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(3),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
                return user;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public IEnumerable<RoleForCreateDto> GetAllRoles()
        {
            try
            {
                List<RoleForCreateDto> lstRole = new List<RoleForCreateDto>();
                var results = _roleManager.Roles;

                foreach (var role in results)
                {
                    lstRole.Add(new RoleForCreateDto
                    {
                        RoleName = role.Name
                    });
                }

                return lstRole;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            try
            {
                List<UserDto> users = new List<UserDto>();
                var results = _userManager.Users;
                foreach (var user in results)
                {
                    users.Add(new UserDto
                    {
                        Username = user.UserName
                    });
                }

                return users;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task<List<string>> GetRolesFromUser(string username)
        {
            try
            {
                List<string> lstRoles = new List<string>();
                var user = await _userManager.FindByEmailAsync(username);
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    lstRoles.Add(role);
                }
                return lstRoles;
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }

        public async Task Registration(UserForCreateDto user)
        {
            try
            {
                var newUser = new IdentityUser
                {
                    UserName = user.Username,
                    Email = user.Username

                };

                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (!result.Succeeded)
                {
                    throw new Exception("Gagal Menambahkan User ");
                }
            }
            catch (Exception ex)
            {

                throw new Exception($"Error: {ex.Message}");
            }
        }
    }
}
