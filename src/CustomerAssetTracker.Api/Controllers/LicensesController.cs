using Microsoft.AspNetCore.Mvc;
using CustomerAssetTracker.Core.Abstractions;
using CustomerAssetTracker.Core;
using CustomerAssetTracker.Api.DTOs;
using AutoMapper;


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

        // GET /api/licenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenseDto>>> GetLicenses()
        {
            var licenses = await _unitOfWork.Licenses.GetAllAsync(l => l.Machine, l => l.Customer);
            var licenseDtos = _mapper.Map<IEnumerable<LicenseDto>>(licenses);
            return Ok(licenseDtos);
        }

        // GET /api/licenses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseDto>> GetLicense(int id)
        {

            var license = await _unitOfWork.Licenses.GetByIdAsync(id, l => l.Machine, l => l.Customer);
            if (license == null)
            {
                return NotFound();
            }

            var licenseDto = _mapper.Map<LicenseDto>(license);
            return Ok(licenseDto);
        }

        // POST /api/licenses
        [HttpPost]
        public async Task<ActionResult<LicenseDto>> CreateLicense(CreateLicenseDto createLicenseDto)
        {
            var license = _mapper.Map<License>(createLicenseDto);

            // Validate Machine and Customer exists before adding reference to License.
            if (license.MachineId.HasValue)
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(license.MachineId.Value);
                if (machine == null)
                {
                    ModelState.AddModelError("MachineId", "Machine with the specified ID does not exist.");
                    return BadRequest(ModelState);
                }
                license.Machine = machine;
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(license.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Customer with the specified ID does not exist.");
                return BadRequest(ModelState);
            }
            license.Customer = customer;


            await _unitOfWork.Licenses.AddAsync(license);
            await _unitOfWork.CompleteAsync();

            var licenseDto = _mapper.Map<LicenseDto>(license);
            return CreatedAtAction(nameof(GetLicense), new { id = license.Id }, licenseDto);
        }

        // PUT /api/licenses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLicense(int id, UpdateLicenseDto updateLicenseDto)
        {
            var license = await _unitOfWork.Licenses.GetByIdAsync(id);

            if (license == null)
            {
                return NotFound();
            }

            _mapper.Map(updateLicenseDto, license);

            // Validate Machine and Customer exists before adding reference to License.
            if (license.MachineId.HasValue)
            {
                var machine = await _unitOfWork.Machines.GetByIdAsync(license.MachineId.Value);
                if (machine == null)
                {
                    ModelState.AddModelError("MachineId", "Machine with the specified ID does not exist..");
                    return BadRequest(ModelState);
                }
                license.Machine = machine;
            } else {
                license.Machine = null; 
            }

            var customer = await _unitOfWork.Customers.GetByIdAsync(license.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError("CustomerId", "Customer with the specified ID does not exist.");
                return BadRequest(ModelState);
            }
            license.Customer = customer;

            _unitOfWork.Licenses.Update(license);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE /api/licenses/{id}
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

        // PATCH /api/licenses/{id}
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
                    ModelState.AddModelError("MachineId", "Machine with the specified ID does not exist.");
                    return BadRequest(ModelState);
                }
                license.MachineId = patchLicenseDto.MachineId.Value;
                license.Machine = machine;
            } else if (patchLicenseDto.MachineId == null) { // explicitly set MachineId to null
                license.MachineId = null;
                license.Machine = null;
            }

            if (patchLicenseDto.CustomerId.HasValue)
            {
                var customer = await _unitOfWork.Customers.GetByIdAsync(patchLicenseDto.CustomerId.Value);
                if (customer == null)
                {
                    ModelState.AddModelError("CustomerId", "Customer with the specified ID does not exist.");
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
