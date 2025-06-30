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
using System.Security.Claims;



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
        [Authorize(Roles = "admin,receptionist")]
        public async Task<ActionResult<List<Appointment>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            return Ok(appointments);
        }

        [HttpGet("my-appointments")]
        [Authorize(Roles = "dentist")]
        public async Task<ActionResult<List<Appointment>>> GetMyAppointments()
        {
            var dentistId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appointments = await _appointmentService.GetAppointmentsByDentist(dentistId);
            return Ok(appointments);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "admin,receptionist,dentist")]
        public async Task<ActionResult<Appointment>> GetById(string id)
        {
            var appointment = await _appointmentService.GetOneById(id);
            if (appointment == null)
                return NotFound();

            return Ok(appointment);
        }

        [HttpPost]
        [Authorize(Roles = "admin,receptionist")]
        public async Task<ActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            var patientExist = await _appointmentService.PatientExists(dto.PatientId);
            if (!patientExist)
                return BadRequest("El paciente no existe");

            var dentistExist = await _appointmentService.DentistExists(dto.DentistId);
            if (!dentistExist)
                return BadRequest("El odontologo no existe");

            var serviceExist = await _appointmentService.ServiceExists(dto.ServiceId);
            if (!serviceExist)
                return BadRequest("El servicio no existe");

            await _appointmentService.CreateAppointment(dto);
            return Ok("Cita creada correctamente.");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin,receptionist")]
        public async Task<ActionResult> Update(string id, [FromBody] UpdateAppointmentDto dto)
        {
            var findApp = await _appointmentService.GetOneById(id);
            if (findApp == null)
                return NotFound();

            await _appointmentService.UpdateApp(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}/complete")]
        [Authorize(Roles = "dentist")]
        public async Task<ActionResult> MarkComplete(string id)
        {
            var dentistId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var appointment = await _appointmentService.GetOneById(id);
            if (appointment == null) return NotFound();

            if (appointment.DentistId != dentistId) return Forbid();

            await _appointmentService.MarkCompleteAsync(id);

            return Ok("Cita marcada como completada.");
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var findApp = await _appointmentService.GetOneById(id);
            if (findApp == null)
                return NotFound();

            await _appointmentService.DeleteAppointment(id);
            return NoContent();
        }

        [HttpGet("detailed")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<AppointmentDetailDto>>> AppointmentWithDetails()
        {
            var appointmentDetails = await _appointmentService.GetDetailedAppointments();
            return Ok(appointmentDetails);
        }
    }
}