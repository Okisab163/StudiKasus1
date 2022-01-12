using AuthServer.Data;
using AuthServer.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IAuthRepo _repository;

        public RolesController(IAuthRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RoleForCreateDto>> GetAllRole()
        {
            try
            {
                Console.WriteLine($"--> Get All Roles .....");
                return Ok(_repository.GetAllRoles());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpPost("Role/{rolename}")]
        public async Task<ActionResult> AddRole(RoleForCreateDto roleForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> Create Roles: {roleForCreateDto.RoleName} .....");
                await _repository.AddRole(roleForCreateDto.RoleName);
                return Ok($"Tambah Role {roleForCreateDto.RoleName} Telah Berhasil");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("UserInRole")]
        public async Task<ActionResult> AddUserToRole(string username, string role)
        {
            try
            {
                Console.WriteLine($"--> Add User: {username} To Roles: {role} .....");
                await _repository.AddUserToRole(username, role);
                return Ok($"Berhasil Menambahkan User: {username}, Dengan Role: {role}");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}
