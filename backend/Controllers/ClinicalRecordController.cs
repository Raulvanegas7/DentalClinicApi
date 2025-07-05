using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        [Authorize(Roles = "Admin,Dentist")]
        public async Task<ActionResult> CreateClinicalRecord([FromBody] CreateClinicalRecordDto dto)
        {
            if (!await _clinicalRecordService.PatientExists(dto.PatientId))
                return BadRequest("El paciente no existe.");

            if (!await _clinicalRecordService.AppointmentExists(dto.AppointmentId))
                return BadRequest("La cita no existe.");

            if (await _clinicalRecordService.GetByAppointmentIdAsync(dto.AppointmentId) != null)
            {
                return BadRequest("Ya existe una historia clínica para esta cita.");
            }

            var newRecord = new ClinicalRecord
            {
                AppointmentId = dto.AppointmentId,
                PatientId = dto.PatientId,
                DentistId = dto.DentistId,
                Diagnosis = dto.Diagnosis,
                Treatment = dto.Treatment,
                Notes = dto.Notes
            };

            await _clinicalRecordService.CreateAsync(newRecord);

            return CreatedAtAction(nameof(GetByAppointmentId),
             new { appointmentId = newRecord.AppointmentId },
             newRecord);
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