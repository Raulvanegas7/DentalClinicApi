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
        public async Task<ActionResult<List<Appointment>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("my-appointments")]
        [Authorize(Roles = "Dentist")]
        public async Task<ActionResult<List<Appointment>>> GetMyAppointments()
        {
            var dentistUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appointments = await _appointmentService.GetAppointmentsByDentist(dentistUserId);
            return Ok(appointments);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<Appointment>> GetById(string id)
        {
            var appointment = await _appointmentService.GetOneById(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var newAppointment = await _appointmentService.CreateAppointment(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = newAppointment.Id }, newAppointment);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult> PartialUpdate(string id, [FromBody] UpdateAppointmentDto dto)
        {
            await _appointmentService.PartialUpdateAsync(id, dto);
            return Ok("Cita actualizada correctamente.");
        }


        [HttpPatch("complete/{id}")]
        [Authorize(Roles = "Dentist")]
        public async Task<ActionResult> MarkComplete(string id)
        {
            var dentistUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _appointmentService.MarkCompleteAsync(id, dentistUserId);

            return Ok("Cita marcada como completada.");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Receptionist")]
        public async Task<ActionResult> Delete(string id)
        {
            var findApp = await _appointmentService.GetOneById(id);
            if (findApp == null)
                return NotFound();

            await _appointmentService.DeleteAppointment(id);
            return NoContent();
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