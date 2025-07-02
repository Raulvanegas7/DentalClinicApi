using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> Create([FromBody] Patient newPatient)
        {
            await _patientService.CreatePatient(newPatient);
            return CreatedAtAction(nameof(GetById), new { id = newPatient.Id }, newPatient);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> Update(string id, Patient updatePatient)
        {
            var existingPatient = await _patientService.GetOneById(id);
            if (existingPatient == null)
                return NotFound();

            updatePatient.Id = id;
            await _patientService.UpdatePatient(id, updatePatient);
            return NoContent();
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