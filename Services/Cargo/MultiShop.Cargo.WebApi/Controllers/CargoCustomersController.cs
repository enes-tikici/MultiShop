using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomersController : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService;

        public CargoCustomersController(ICargoCompanyService cargoCompanyService)
        {
            _cargoCustomerService = cargoCompanyService;
        }

        [HttpGet]
        public IActionResult CargoCustomerList()
        {
            var values = _cargoCustomerService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoCustomerById(int id)
        {
            var value = _cargoCustomerService.TGetById(id);
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateCargoCustomer(CreateCargoCustomerDto dto)
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                District = dto.District,
                City = dto.City,
                Adress = dto.Adress
            };
            _cargoCustomerService.TInsert(cargoCustomer);
            return Ok("Cargo müşterisi başarıyla oluşturuldu");
        }

        [HttpPut]
        public IActionResult UpdateCargoCustomer(UpdateCargoCustomerDto dto)
        {
            CargoCustomer cargoCustomer = new CargoCustomer()
            {
                CargoCustomerId = dto.CargoCustomerId,
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                District = dto.District,
                City = dto.City,
                Adress = dto.Adress
            };
            _cargoCustomerService.TUpdate(cargoCustomer);
            return Ok("Cargo müşterisi başarıyla güncellendi");
        }

        [HttpDelete]
        public IActionResult RemoveCargoCustomer(int id)
        {
            _cargoCustomerService.TDelete(id);
            return Ok("Cargo müşterisi başarıyla silindi");
        }
    }
}
