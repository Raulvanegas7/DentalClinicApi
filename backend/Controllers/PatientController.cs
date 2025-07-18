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
    public class PatientController : ControllerBase
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult<Patient>> GetById(string id)
        {
            var patient = await _patientService.GetOneById(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> RegisterPatientWithUser([FromBody] CreatePatientDto dto)
        {
            await _patientService.RegisterPatientWithUserAsync(dto);
            return Ok(new { message = "Registro exitoso"});
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> Update(string id, UpdatePatientDto dto)
        {
            var existingPatient = await _patientService.GetOneById(id);
            if (existingPatient == null)
                return NotFound();
            
            await _patientService.PartialUpdateAsync(id, dto);
            return Ok("Paciente actualizado con exito");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var existingPatient = await _patientService.GetOneById(id);
            if (existingPatient == null)
                return NotFound();

            await _patientService.DeletePatient(id);
            return NoContent();
        }
    }
}