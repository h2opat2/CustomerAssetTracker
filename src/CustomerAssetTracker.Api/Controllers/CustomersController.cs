    using Microsoft.AspNetCore.Mvc;
    using CustomerAssetTracker.Core.Abstractions; // Pro IUnitOfWork
    using CustomerAssetTracker.Core; // Pro Customer entitu
    using CustomerAssetTracker.Api.DTOs; // Pro DTOs
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper; 

    namespace CustomerAssetTracker.Api.Controllers
{
    // Komentář: Atributy pro API kontroler.
    // [ApiController] označuje, že třída je API kontroler.
    // [Route("api/[controller]")] definuje základní cestu pro tento kontroler (např. /api/customers).
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork; // Vstříknutá instance Unit of Work
        private readonly IMapper _mapper; // Vstříknutá instance AutoMapperu

        // Komentář: Konstruktor pro Dependency Injection.
        // ASP.NET Core automaticky vstříkne instanci IUnitOfWork.
        public CustomersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // Komentář: GET /api/customers
        // Získá všechny zákazníky.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync(c => c.Machines, c => c.Licenses);
            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            return Ok(customerDtos); // Vrací HTTP 200 OK s daty
        }

        // Komentář: GET /api/customers/{id}
        // Získá zákazníka podle ID.
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id, c => c.Machines, c => c.Licenses);

            if (customer == null)
            {
                return NotFound(); // Vrací HTTP 404 Not Found
            }

            var customerDto = _mapper.Map<CustomerDto>(customer);

            return Ok(customerDto); // Vrací HTTP 200 OK s daty
        }

        // Komentář: POST /api/customers
        // Vytvoří nového zákazníka.
        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            // Komentář: Zde se provádí automatická validace modelu díky [ApiController] atributu.
            // Pokud validace selže, ASP.NET Core automaticky vrátí HTTP 400 Bad Request.

            // var customer = new Customer
            // {
            //     Name = createCustomerDto.Name,
            //     Address = createCustomerDto.Address,
            //     IsForeign = createCustomerDto.IsForeign
            // };

            // Komentář: Používáme AutoMapper pro mapování CreateCustomerDto na Customer entitu.
            var customer = _mapper.Map<Customer>(createCustomerDto);


            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.CompleteAsync(); // Uloží změny do databáze

            // Komentář: Vrací HTTP 201 Created s hlavičkou Location a nově vytvořeným zdrojem.
            var customerDto = new CustomerDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Address = customer.Address,
                IsForeign = customer.IsForeign,
                MachineCount = 0, // Nový zákazník nemá zatím stroje/licence
                LicenseCount = 0
            };

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customerDto);
        }

        // Komentář: PUT /api/customers/{id}
        // Aktualizuje existujícího zákazníka.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Zákazník nenalezen
            }

            // Aktualizace vlastností z DTO
            // customer.Name = updateCustomerDto.Name;
            // customer.Address = updateCustomerDto.Address;
            // customer.IsForeign = updateCustomerDto.IsForeign;

            // Komentář: Používáme AutoMapper pro aktualizaci existující entity z DTO.
            // AutoMapper aktualizuje existující objekt 'customer' z 'updateCustomerDto'.
            _mapper.Map(updateCustomerDto, customer);

            _unitOfWork.Customers.Update(customer);
            await _unitOfWork.CompleteAsync(); // Uloží změny

            return NoContent(); // Vrací HTTP 204 No Content (úspěšná aktualizace bez obsahu)
        }

        // Komentář: PATCH /api/customers/{id}
        // Provádí částečnou aktualizaci zákazníka.
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCustomer(int id, PatchCustomerDto patchCustomerDto)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Zákazník nenalezen
            }

            // Komentář: Aktualizuje pouze ta pole, která byla v DTO skutečně poslána.
            // Operátor '??' nebo kontrola '.HasValue' u nullable typů.
            if (patchCustomerDto.Name != null)
            {
                customer.Name = patchCustomerDto.Name;
            }
            if (patchCustomerDto.Address != null)
            {
                customer.Address = patchCustomerDto.Address;
            }
            if (patchCustomerDto.IsForeign.HasValue) // Pro nullable bool je nutné použít .HasValue
            {
                customer.IsForeign = patchCustomerDto.IsForeign.Value; // A .Value pro získání skutečné hodnoty
            }

            _unitOfWork.Customers.Update(customer); // Označí entitu jako změněnou
            await _unitOfWork.CompleteAsync(); // Uloží změny

            return NoContent(); // Vrací HTTP 204 No Content (úspěšná aktualizace bez obsahu)
        }

        // Komentář: DELETE /api/customers/{id}
        // Smaže zákazníka.
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(); // Zákazník nenalezen
            }

            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.CompleteAsync(); // Uloží změny

            return NoContent(); // Vrací HTTP 204 No Content (úspěšné smazání bez obsahu)
        }
    }
}
    