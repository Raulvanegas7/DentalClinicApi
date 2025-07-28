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

        [HttpPost]
        [Authorize(Roles = "Admin,Dentist")]
        public async Task<ActionResult<ClinicalRecord>> CreateClinicalRecord([FromBody] CreateClinicalRecordDto dto)
        {
            var newRecord = await _clinicalRecordService.CreateClinicalRecordAsync(dto);

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