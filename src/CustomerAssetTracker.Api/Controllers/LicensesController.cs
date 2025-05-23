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
    public class LicensesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LicensesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Komentář: GET /api/licenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenseDto>>> GetLicenses()
        {
            var licenses = await _unitOfWork.Licenses.GetAllAsync(
                l => l.Machine,
                l => l.Customer
            );
            var licenseDtos = _mapper.Map<IEnumerable<LicenseDto>>(licenses);
            return Ok(licenseDtos);
        }

        // Komentář: GET /api/licenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseDto>> GetLicense(int id)
        {
            // Komentář: Stejně jako výše, pro MachineName a CustomerName potřebujeme eager loading.
            var license = await _unitOfWork.Licenses.GetByIdAsync(
                id,
                l => l.Machine,
                l => l.Customer
            );
            if (license == null)
            {
                return NotFound();
            }

            var licenseDto = _mapper.Map<LicenseDto>(license);
            return Ok(licenseDto);
        }

        // Komentář: POST /api/licenses
        [HttpPost]
        public async Task<ActionResult<LicenseDto>> CreateLicense(CreateLicenseDto createLicenseDto)
        {
            var license = _mapper.Map<License>(createLicenseDto);

            // Komentář: Ověření existence Machine a Customer před přidáním licence.
            // Toto je základní validace referenční integrity.
            if (license.MachineId.HasValue)
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(license.MachineId.Value);
                if (machine == null)
                {
                    ModelState.AddModelError("MachineId", "Zadaný stroj neexistuje.");
                    return BadRequest(ModelState);
                }
                license.Machine = machine; // Připojení entity pro správné uložení vazby
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(license.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Zadaný zákazník neexistuje.");
                return BadRequest(ModelState);
            }
            license.Customer = customer; // Připojení entity pro správné uložení vazby


            await _unitOfWork.Licenses.AddAsync(license);
            await _unitOfWork.CompleteAsync();

            var licenseDto = _mapper.Map<LicenseDto>(license);
            return CreatedAtAction(nameof(GetLicense), new { id = license.Id }, licenseDto);
        }

        // Komentář: PUT /api/licenses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLicense(int id, UpdateLicenseDto updateLicenseDto)
        {
            var license = await _unitOfWork.Licenses.GetByIdAsync(id);

            if (license == null)
            {
                return NotFound();
            }

            _mapper.Map(updateLicenseDto, license);

            // Komentář: Ověření existence Machine a Customer před aktualizací licence.
            if (license.MachineId.HasValue)
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(license.MachineId.Value);
                if (machine == null)
                {
                    ModelState.AddModelError("MachineId", "Zadaný stroj neexistuje.");
                    return BadRequest(ModelState);
                }
                license.Machine = machine;
            } else {
                license.Machine = null; // Pokud MachineId je null, odpojit stroj
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(license.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Zadaný zákazník neexistuje.");
                return BadRequest(ModelState);
            }
            license.Customer = customer;

            _unitOfWork.Licenses.Update(license);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // Komentář: DELETE /api/licenses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicense(int id)
        {
            var license = await _unitOfWork.Licenses.GetByIdAsync(id);

            if (license == null)
            {
                return NotFound();
            }

            _unitOfWork.Licenses.Delete(license);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // Komentář: PATCH /api/licenses/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchLicense(int id, PatchLicenseDto patchLicenseDto)
        {
            var license = await _unitOfWork.Licenses.GetByIdAsync(id);

            if (license == null)
            {
                return NotFound();
            }

            if (patchLicenseDto.Software != null) license.Software = patchLicenseDto.Software;
            if (patchLicenseDto.Version != null) license.Version = patchLicenseDto.Version;
            if (patchLicenseDto.Vendor != null) license.Vendor = patchLicenseDto.Vendor;
            if (patchLicenseDto.MaintenanceContract.HasValue) license.MaintenanceContract = patchLicenseDto.MaintenanceContract.Value;
            if (patchLicenseDto.LicenseType != null) license.Type = Enum.Parse<License.LicenseType>(patchLicenseDto.LicenseType);
            if (patchLicenseDto.MachineId.HasValue)
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(patchLicenseDto.MachineId.Value);
                if (machine == null)
                {
                    ModelState.AddModelError("MachineId", "Zadaný stroj neexistuje.");
                    return BadRequest(ModelState);
                }
                license.MachineId = patchLicenseDto.MachineId.Value;
                license.Machine = machine;
            } else if (patchLicenseDto.MachineId == null) { // Pokud je explicitně posláno null pro MachineId
                license.MachineId = null;
                license.Machine = null;
            }

            if (patchLicenseDto.CustomerId.HasValue)
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(patchLicenseDto.CustomerId.Value);
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerId", "Zadaný zákazník neexistuje.");
                    return BadRequest(ModelState);
                }
                license.CustomerId = patchLicenseDto.CustomerId.Value;
                license.Customer = customer;
            }


            _unitOfWork.Licenses.Update(license);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
