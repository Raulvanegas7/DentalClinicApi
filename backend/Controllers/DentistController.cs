using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dtos;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DentistController : ControllerBase
    {
        private readonly DentistService _dentistService;

        public DentistController(DentistService dentistService)
        {
            _dentistService = dentistService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Dentist>>> GetAll()
        {
            var dentists = await _dentistService.GetAllDentists();
            return Ok(dentists);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Dentist>> GetById(string id)
        {
            var dentist = await _dentistService.GetOneById(id);
            if (dentist == null)
                return NotFound();

            return Ok(dentist);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> RegisterUserWithDentist([FromBody] CreateDentistDto dto)
        {
            var token = await _dentistService.RegisterUserWithDentistAsync(dto);
            if (token == null)
                return BadRequest("El dentista o usuario ya existe.");
                
            return Ok(new { message = "Dentista registrado correctamente.", token });
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateDentist(string id, [FromBody] UpdateDentistDto dto)
        {
            var findDentist = await _dentistService.GetOneById(id);
            if (findDentist == null)
                return NotFound("El odontólogo no existe.");

            await _dentistService.PartialUpdateAsync(id, dto);
            return Ok("Odontólogo actualizado correctamente.");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var findDentist = await _dentistService.GetOneById(id);
            if (findDentist == null)
                return NotFound();

            await _dentistService.DeleteDentist(id);
            return NoContent();
        }
    }
}