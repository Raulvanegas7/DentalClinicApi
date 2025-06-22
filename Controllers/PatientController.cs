using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;


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
        [SwaggerOperation(Summary = "Obtener todos los pacientes",
            Description = "Retorna una lista de todos los pacientes registrados")]
        [SwaggerResponse(200, "Lista de pacientes obtenida exitosamente", typeof(List<Patient>))]
        public async Task<ActionResult<List<Patient>>> GetAll()
        {
            var patients = await _patientService.GetAllPatients();
            return Ok(patients);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener un paciente por ID")]
        [SwaggerResponse(200, "Paciente encontrado", typeof(Patient))]
        [SwaggerResponse(404, "Paciente no encontrado")]
        public async Task<ActionResult<Patient>> GetById(string id)
        {
            var patient = await _patientService.GetOneById(id);
            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Registrar un nuevo paciente")]
        [SwaggerResponse(201, "Paciente creado exitosamente")]
        [SwaggerResponse(400, "Datos inválidos")]   
        public async Task<ActionResult> Create([FromBody] Patient newPatient)
        {
            await _patientService.CreatePatient(newPatient);
            return CreatedAtAction(nameof(GetById), new { id = newPatient.Id }, newPatient);
        }

        [HttpPut("{id}")]
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