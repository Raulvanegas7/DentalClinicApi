using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Dtos;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult<string>> SignUp([FromBody] RegisterDto dto)
        {
            var token = await _userService.RegisterUser(dto);
            if (token == null)
                return BadRequest("El correo ya está registrado.");

            return Ok(new { token });
        }

        [HttpPost("signin")]
        public async Task<ActionResult<string>> SignIn([FromBody] LoginDto dto)
        {
            var token = await _userService.Login(dto);
            if (token == null)
                return Unauthorized("Credenciales inválidas.");

            return Ok(new { token });
        }
    }
}