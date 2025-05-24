using Microsoft.AspNetCore.Mvc;
using CustomerAssetTracker.Core.Abstractions;
using CustomerAssetTracker.Core;
using CustomerAssetTracker.Api.DTOs;
using AutoMapper;


namespace CustomerAssetTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MachinesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MachinesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET /api/machines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MachineDto>>> GetMachines()
        {
            var machines = await _unitOfWork.Machines.GetAllAsync(
                m => m.Customer,
                m => m.Licenses,
                m => m.ServiceRecords
            );
            var machineDtos = _mapper.Map<IEnumerable<MachineDto>>(machines);
            return Ok(machineDtos);
        }

        // GET /api/machines/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<MachineDto>> GetMachine(int id)
        {
            var machine = await _unitOfWork.Machines.GetByIdAsync(
                id,
                m => m.Customer,
                m => m.Licenses,
                m => m.ServiceRecords
            );
            
            if (machine == null)
            {
                return NotFound();
            }

            var machineDto = _mapper.Map<MachineDto>(machine);
            return Ok(machineDto);
        }

        // POST /api/machines
        [HttpPost]
        public async Task<ActionResult<MachineDto>> CreateMachine(CreateMachineDto createMachineDto)
        {
            var machine = _mapper.Map<Machine>(createMachineDto);

            await _unitOfWork.Machines.AddAsync(machine);
            await _unitOfWork.CompleteAsync();

            var machineDto = _mapper.Map<MachineDto>(machine);
            return CreatedAtAction(nameof(GetMachine), new { id = machine.Id }, machineDto);
        }

        // PUT /api/machines/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMachine(int id, UpdateMachineDto updateMachineDto)
        {
            var machine = await _unitOfWork.Machines.GetByIdAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            _mapper.Map(updateMachineDto, machine);

            _unitOfWork.Machines.Update(machine);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // DELETE /api/machines/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var machine = await _unitOfWork.Machines.GetByIdAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            _unitOfWork.Machines.Delete(machine);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        // PATCH /api/machines/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchMachine(int id, PatchMachineDto patchMachineDto)
        {
            var machine = await _unitOfWork.Machines.GetByIdAsync(id);

            if (machine == null)
            {
                return NotFound();
            }

            //Manually mapping properties from PatchMachineDto to Machine
            if (patchMachineDto.Name != null) machine.Name = patchMachineDto.Name;
            if (patchMachineDto.SerialNumber != null) machine.SerialNumber = patchMachineDto.SerialNumber;
            if (patchMachineDto.Manufacturer != null) machine.Manufacturer = patchMachineDto.Manufacturer;
            if (patchMachineDto.PurchaseDate.HasValue) machine.PurchaseDate = patchMachineDto.PurchaseDate.Value;
            if (patchMachineDto.MachineType != null) machine.MachineType = Enum.Parse<Machine.MachineTypes>(patchMachineDto.MachineType);
            if (patchMachineDto.CustomerId.HasValue) machine.CustomerId = patchMachineDto.CustomerId.Value;

            _unitOfWork.Machines.Update(machine);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
