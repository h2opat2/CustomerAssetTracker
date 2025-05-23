using Microsoft.AspNetCore.Mvc;
using CustomerAssetTracker.Core.Abstractions;
using CustomerAssetTracker.Core;
using CustomerAssetTracker.Api.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore; // Pro .Include() - dočasně zde pro ukázku, ideálně v repozitáři

namespace CustomerAssetTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceRecordsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServiceRecordsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Komentář: GET /api/servicerecords
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceRecordDto>>> GetServiceRecords()
        {
            var serviceRecords = await _unitOfWork.ServiceRecords.GetAllAsync(sr => sr.Machine);
            var serviceRecordDtos = _mapper.Map<IEnumerable<ServiceRecordDto>>(serviceRecords);
            return Ok(serviceRecordDtos);
        }

        // Komentář: GET /api/servicerecords/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceRecordDto>> GetServiceRecord(int id)
        {
            var serviceRecord = await _unitOfWork.ServiceRecords.GetByIdAsync(id, sr => sr.Machine);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            var serviceRecordDto = _mapper.Map<ServiceRecordDto>(serviceRecord);
            return Ok(serviceRecordDto);
        }

        // Komentář: POST /api/servicerecords
        [HttpPost]
        public async Task<ActionResult<ServiceRecordDto>> CreateServiceRecord(CreateServiceRecordDto createServiceRecordDto)
        {
            var serviceRecord = _mapper.Map<ServiceRecord>(createServiceRecordDto);

            // Komentář: Ověření existence Machine před přidáním servisního záznamu.
            var machine = await _unitOfWork.Machines.GetByIdAsync(serviceRecord.MachineId);
            if (machine == null)
            {
                ModelState.AddModelError("MachineId", "Zadaný stroj neexistuje.");
                return BadRequest(ModelState);
            }
            serviceRecord.Machine = machine; // Připojení entity pro správné uložení vazby

            await _unitOfWork.ServiceRecords.AddAsync(serviceRecord);
            await _unitOfWork.CompleteAsync();

            var serviceRecordDto = _mapper.Map<ServiceRecordDto>(serviceRecord);
            return CreatedAtAction(nameof(GetServiceRecord), new { id = serviceRecord.Id }, serviceRecordDto);
        }

        // Komentář: PUT /api/servicerecords/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateServiceRecord(int id, UpdateServiceRecordDto updateServiceRecordDto)
        {
            var serviceRecord = await _unitOfWork.ServiceRecords.GetByIdAsync(id);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            _mapper.Map(updateServiceRecordDto, serviceRecord);

            // Komentář: Ověření existence Machine před aktualizací.
            var machine = await _unitOfWork.Machines.GetByIdAsync(serviceRecord.MachineId);
            if (machine == null)
            {
                ModelState.AddModelError("MachineId", "Zadaný stroj neexistuje.");
                return BadRequest(ModelState);
            }
            serviceRecord.Machine = machine;

            _unitOfWork.ServiceRecords.Update(serviceRecord);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // Komentář: DELETE /api/servicerecords/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRecord(int id)
        {
            var serviceRecord = await _unitOfWork.ServiceRecords.GetByIdAsync(id);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            _unitOfWork.ServiceRecords.Delete(serviceRecord);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // Komentář: PATCH /api/servicerecords/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchServiceRecord(int id, PatchServiceRecordDto patchServiceRecordDto)
        {
            var serviceRecord = await _unitOfWork.ServiceRecords.GetByIdAsync(id);

            if (serviceRecord == null)
            {
                return NotFound();
            }

            if (patchServiceRecordDto.Date.HasValue) serviceRecord.Date = patchServiceRecordDto.Date.Value;
            if (patchServiceRecordDto.Technician != null) serviceRecord.Technician = patchServiceRecordDto.Technician;
            if (patchServiceRecordDto.Text != null) serviceRecord.Text = patchServiceRecordDto.Text;
            if (patchServiceRecordDto.MachineId.HasValue)
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(patchServiceRecordDto.MachineId.Value);
                if (machine == null)
                {
                    ModelState.AddModelError("MachineId", "Zadaný stroj neexistuje.");
                    return BadRequest(ModelState);
                }
                serviceRecord.MachineId = patchServiceRecordDto.MachineId.Value;
                serviceRecord.Machine = machine;
            }

            _unitOfWork.ServiceRecords.Update(serviceRecord);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
