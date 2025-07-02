using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DentalClinicApi.Models;
using DentalClinicApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DentalClinicApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ServiceService _serviceService;

        public ServiceController(ServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<Service>>> GetAll()
        {
            var services = await _serviceService.GetAllServices();
            return Ok(services);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Service>> GetById(string id)
        {
            var service = await _serviceService.GetOneById(id);
            if (service == null)
                return NotFound();

            return Ok(service);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] Service newService)
        {
            await _serviceService.CreateService(newService);
            return CreatedAtAction(nameof(GetById), new { id = newService.Id }, newService);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(string id, Service updateService)
        {
            var findService = await _serviceService.GetOneById(id);
            if (findService == null)
                return NotFound();

            updateService.Id = id;
            await _serviceService.UpdateService(id, updateService);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(string id)
        {
            var findService = await _serviceService.GetOneById(id);
            if (findService == null)
                return NotFound();

            await _serviceService.DeleteService(id);
            return NoContent();
        }
    }
}