using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DentalClinicApi.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
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
        public async Task<ActionResult> create([FromBody] Appointment newAppointment)
        {
            var findpatient = await _appointmentService.GetOneById(newAppointment.PatientId);
            if (findpatient == null)
                return BadRequest("El paciente no existe");

            var findDentist = await _appointmentService.GetOneById(newAppointment.DentistId);
            if (findDentist == null)
                return BadRequest("El odontologo no existe");

            var findService = await _appointmentService.GetOneById(newAppointment.ServiceId);
            if (findService == null)
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
    }
}