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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using backend.Enums;
using backend.Dtos;



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
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult<List<Appointment>>> GetAllAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("my-appointments")]
        [Authorize(Roles = "Dentist")]
        public async Task<ActionResult<List<Appointment>>> GetMyAppointments()
        {
            var dentistUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(dentistUserId))
            {
                throw new ArgumentException("El ID del dentista no puede ser nulo.");
            }

            var appointments = await _appointmentService.GetAppointmentsByDentist(dentistUserId);
            return Ok(appointments);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(string id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            var newAppointment = await _appointmentService.CreateAppointmentAsync(dto);

            return CreatedAtAction(nameof(GetAppointmentById),
                new { id = newAppointment.Id }, newAppointment);
        }


        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult> UpdateAppointment(string id, [FromBody] UpdateAppointmentDto dto)
        {
            await _appointmentService.UpdateAppointmentAsync(id, dto);
            return Ok("Cita actualizada correctamente.");
        }


        [HttpPatch("complete/{id}")]
        [Authorize(Roles = "Dentist")]
        public async Task<ActionResult> MarkComplete(string id)
        {
            var dentistUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(dentistUserId))
            {
                throw new ArgumentException("El ID del dentista no puede ser nulo.");
            }

            await _appointmentService.MarkCompleteAsync(id, dentistUserId);

            return Ok("Cita marcada como completada.");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> Delete(string id)
        {
            var findApp = await _appointmentService.GetAppointmentByIdAsync(id);
            if (findApp == null)
                return NotFound();

            await _appointmentService.DeleteAppointment(id);
            return Ok("Se ha elimindado correctamente");
        }


        [HttpGet("detailed")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult<List<AppointmentDetailDto>>> AppointmentWithDetails()
        {
            var appointmentDetails = await _appointmentService.GetDetailedAppointments();
            return Ok(appointmentDetails);
        }
    }
}