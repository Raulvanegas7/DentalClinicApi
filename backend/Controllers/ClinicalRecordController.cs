using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Dtos;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
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
        public async Task<ActionResult<List<ClinicalRecord>>> GetByPatientId(string patientId)
        {
            var records = await _clinicalRecordService.GetByPatientIdAsync(patientId);
            return Ok(records);
        }

        [HttpGet("byAppointmentId/{appointmentId}")]
        public async Task<ActionResult<ClinicalRecord?>> GetByAppointmentId(string appointmentId)
        {
            var record = await _clinicalRecordService.GetByAppointmentIdAsync(appointmentId);
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<ActionResult> CreateClinicalRecord([FromBody] CreateClinicalRecordDto dto)
        {
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
            return CreatedAtAction(nameof(GetByAppointmentId), new { appointmentId = newRecord.AppointmentId}, newRecord);
        }
    }
}