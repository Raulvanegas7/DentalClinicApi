using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Dtos;
using DentalClinicApi.Dtos;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicalRecordController : ControllerBase
    {
        private readonly ClinicalRecordService _clinicalRecordService;

        public ClinicalRecordController(ClinicalRecordService clinicalRecordService)
        {
            _clinicalRecordService = clinicalRecordService;
        }

        [HttpGet("byPatientId/{patientId}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<List<ClinicalRecord>>> GetByPatientId(string patientId)
        {
            var records = await _clinicalRecordService.GetByPatientIdAsync(patientId);
            return Ok(records);
        }

        [HttpGet("byPatientDetailed/{patientId}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<ClinicalRecord>> GetByPatientDetailed(string patientId)
        {
            var clinicalRecordDetailed = await _clinicalRecordService.GetBytPatientDetailAsync(patientId);
            return Ok(clinicalRecordDetailed);
        }


        [HttpGet("byAppointmentDetailed/{appointmentId}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<ClinicalRecord>> GetByAppointmentDetailed(string appointmentId)
        {
            var clinicalRecordDetailed = await _clinicalRecordService.GetByAppointmentDetailAsync(appointmentId);
            return Ok(clinicalRecordDetailed);
        }


        [HttpGet("byAppointmentId/{appointmentId}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<ClinicalRecord?>> GetByAppointmentId(string appointmentId)
        {
            var record = await _clinicalRecordService.GetByAppointmentIdAsync(appointmentId);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<List<ClinicalRecord>>> GetAllClinicalRecords()
        {
            var allrecords = await _clinicalRecordService.GetAllClinicalRecordsAsync();
            return Ok(allrecords);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Dentist")]
        public async Task<ActionResult<ClinicalRecord>> GetById(string id)
        {
            var record = await _clinicalRecordService.GetByIdAsync(id);
            return Ok(record);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,Dentist")]
        public async Task<ActionResult<ClinicalRecord>> CreateClinicalRecord([FromBody] CreateClinicalRecordDto dto)
        {
            var loggedUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(loggedUserId) || string.IsNullOrEmpty(userRole))
                return Unauthorized("No se pudo obtener la información del usuario logueado.");

            var result = await _clinicalRecordService.CreateClinicalRecordAsync(dto, loggedUserId, userRole);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);


        }

        [HttpGet("detail/{id}")]
        [Authorize(Roles = "Admin,Receptionist,Dentist")]
        public async Task<ActionResult<ClinicalRecordDetailedDto>> GetClinicalWithDetails(string id)
        {
            var findCR = await _clinicalRecordService.GetClinicalWithDetailsAsync(id);
            if (findCR == null) return NotFound();
            return Ok(findCR);
        }
    }
}