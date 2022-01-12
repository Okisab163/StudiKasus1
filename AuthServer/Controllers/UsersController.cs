using AuthServer.Data;
using AuthServer.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAuthRepo _repository;

        public UsersController(IAuthRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                Console.WriteLine($"--> Get All User .....");
                return Ok(_repository.GetAllUsers());
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [HttpGet("RolesByUser/{username}")]
        public async Task<ActionResult<List<string>>> GetRolesFromUser(string username)
        {
            try
            {
                Console.WriteLine($"--> Get Roles By User From: {username} .....");
                var results = await _repository.GetRolesFromUser(username);
                return Ok(results);
            }
            catch (Exception ex)
            {

                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
