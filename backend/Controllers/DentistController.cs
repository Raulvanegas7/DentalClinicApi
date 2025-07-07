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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] CreateDentistDto dto)
        {
            var newDentist = await _dentistService.CreateDentist(dto);
            return CreatedAtAction(nameof(GetById), new { id = newDentist.Id }, newDentist);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(string id, [FromBody] Dentist updateDentist)
        {
            var findDentist = await _dentistService.GetOneById(id);
            if (findDentist == null)
                return NotFound();

            updateDentist.Id = id;
            await _dentistService.UpdateDentist(id, updateDentist);
            return NoContent();
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