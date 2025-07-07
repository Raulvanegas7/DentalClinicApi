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
            var dentistId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var appointments = await _appointmentService.GetAppointmentsByDentist(dentistId);
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
            if (dto.Date <= DateTime.UtcNow)
            {
                return BadRequest("No se puede agendar una cita en el pasado.");
            }

            var localHour = dto.Date.ToLocalTime().TimeOfDay;
            if (localHour < TimeSpan.FromHours(8) || localHour >= TimeSpan.FromHours(18))
            {
                return BadRequest("El horario permitido es de 8:00 AM a 6:00 PM hora Colombia.");
            }

            if (!await _appointmentService.IsDentistAvailableAsync(dto.DentistId, dto.Date))
            {
                return BadRequest("El dentista ya tiene una cita en ese horario.");
            }

            if (!await _appointmentService.IsPatientAvailableAsync(dto.PatientId, dto.Date))
            {
                return BadRequest("El paciente ya tiene una cita en ese horario.");
            }

            var patientExist = await _appointmentService.PatientExists(dto.PatientId);
            if (!patientExist)
                return BadRequest("El paciente no existe");

            var dentistExist = await _appointmentService.DentistExists(dto.DentistId);
            if (!dentistExist)
                return BadRequest("El odontologo no existe");

            var serviceExist = await _appointmentService.ServiceExists(dto.ServiceId);
            if (!serviceExist)
                return BadRequest("El servicio no existe");

            var newAppointment = await _appointmentService.CreateAppointment(dto);

            return CreatedAtAction(nameof(GetById),
                new { id = newAppointment.Id }, newAppointment);
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult> PartialUpdate(string id, [FromBody] UpdateAppointmentDto dto)
        {
            var appointment = await _appointmentService.GetOneById(id);
            if (appointment == null)
                return NotFound();

            if (dto.Date.HasValue)
            {
                if (dto.Date <= DateTime.UtcNow)
                    return BadRequest("No se puede mover la cita al pasado.");

                var localHour = dto.Date.Value.ToLocalTime().TimeOfDay;
                if (localHour < TimeSpan.FromHours(8) || localHour >= TimeSpan.FromHours(18))
                {
                    return BadRequest("El horario permitido es de 8:00 AM a 6:00 PM hora Colombia.");
                }

                if (!await _appointmentService.IsDentistAvailableAsync(appointment.DentistId, dto.Date.Value))
                    return BadRequest("El dentista no está disponible en ese horario.");

                if (!await _appointmentService.IsPatientAvailableAsync(appointment.PatientId, dto.Date.Value))
                    return BadRequest("El paciente no está disponible en ese horario.");
            }

            await _appointmentService.PartialUpdateBasicAsync(id, dto);

            return Ok("Cita actualizada correctamente.");
        }


        [HttpPatch("complete/{id}")]
        [Authorize(Roles = "Dentist")]
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