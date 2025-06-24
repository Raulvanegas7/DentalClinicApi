using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Dtos;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Appointment>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(string id)
        {
            var appointment = await _appointmentService.GetOneById(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Appointment newAppointment)
        {
            var patientExist = await _appointmentService.PatientExists(newAppointment.PatientId);
            if (!patientExist)
                return BadRequest("El paciente no existe");

            var dentistExist = await _appointmentService.DentistExists(newAppointment.DentistId);
            if (!dentistExist)
                return BadRequest("El odontologo no existe");

            var serviceExist = await _appointmentService.ServiceExists(newAppointment.ServiceId);
            if (!serviceExist)
                return BadRequest("El servicio no existe");

            await _appointmentService.CreateAppointment(newAppointment);
            return CreatedAtAction(nameof(GetById), new { id = newAppointment.Id }, newAppointment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] Appointment updateAppointment)
        {
            var findApp = await _appointmentService.GetOneById(id);
            if (findApp == null)
                return NotFound();

            updateAppointment.Id = id;
            await _appointmentService.UpdateApp(id, updateAppointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var findApp = await _appointmentService.GetOneById(id);
            if (findApp == null)
                return NotFound();

            await _appointmentService.DeleteAppointment(id);
            return NoContent();
        }

        [HttpGet("detailed")]
        public async Task<ActionResult<List<AppointmentDetailDto>>> AppointmentWithDetails()
        {
            var appointmentDetails = await _appointmentService.GetDetailedAppointments();
            return Ok(appointmentDetails);
        } 
    }
}