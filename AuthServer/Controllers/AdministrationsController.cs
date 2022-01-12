using AuthServer.Data;
using AuthServer.Dtos;
using AuthServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationsController : ControllerBase
    {
        private readonly IAuthRepo _repository;

        public AdministrationsController(IAuthRepo repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Registration(UserForCreateDto userForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> User Registration With Username: {userForCreateDto.Username} .....");
                await _repository.Registration(userForCreateDto);
                return Ok($"Registrasi User: {userForCreateDto.Username} Telah Berhasil");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("Authentication")]
        public async Task<ActionResult<User>> Authentication(UserForCreateDto userForCreateDto)
        {
            try
            {
                Console.WriteLine($"--> User Login With Username: {userForCreateDto.Username} .....");
                var user = await _repository.Authentication(userForCreateDto.Username, userForCreateDto.Password);
                if (user == null)
                {
                    return BadRequest("Username / Password Tidak Tepat");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


    }
}
