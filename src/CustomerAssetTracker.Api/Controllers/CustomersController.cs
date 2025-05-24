    using Microsoft.AspNetCore.Mvc;
    using CustomerAssetTracker.Core.Abstractions; 
    using CustomerAssetTracker.Core;
    using CustomerAssetTracker.Api.DTOs; 
    using AutoMapper; 

    namespace CustomerAssetTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync(c => c.Machines, c => c.Licenses);
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return Ok(customerDtos); // Return HTTP 200 OK with customer data
        }

        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id, c => c.Machines, c => c.Licenses);

            if (customer == null)
            {
                return NotFound(); // Return HTTP 404 Not Found
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return Ok(customerDto); // Return HTTP 200 OK with customer{id} data
        }

        // POST /api/customers
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);

            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync(); // Save changes to the database


            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                IsForeign = customer.IsForeign,
                MachineCount = 0, //New customer has no machines initially
                LicenseCount = 0 //New customer has no licenses initially
            };

            // Returns HTTP 201 Created with the location of the newly created customer.
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customerDto);
        }

        // PUT /api/customers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Return HTTP 404 Not Found
            }

            _mapper.Map(updateCustomerDto, customer);

            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync(); // Save changes to the database

            return NoContent(); // Returns HTTP 204 No Content (successful update without content)
        }

        // PATCH /api/customers/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCustomer(int id, PatchCustomerDto patchCustomerDto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Return HTTP 404 Not Found
            }

            // Manually mapping properties from PatchCustomerDto to Customer
            // Only update properties that are not null in the patch DTO
            if (patchCustomerDto.Name != null)
            {
                customer.Name = patchCustomerDto.Name;
            }
            if (patchCustomerDto.Address != null)
            {
                customer.Address = patchCustomerDto.Address;
            }
            if (patchCustomerDto.IsForeign.HasValue)
            {
                customer.IsForeign = patchCustomerDto.IsForeign.Value;
            }

            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync(); // Save changes to the database

            return NoContent(); // Returns HTTP 204 No Content (successful update without content)
        }

        // DELETE /api/customers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Return HTTP 404 Not Found
            }

            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.CompleteAsync(); // Save changes to the database

            return NoContent(); // Returns HTTP 204 No Content (successful update without content)
        }

    }
}
    