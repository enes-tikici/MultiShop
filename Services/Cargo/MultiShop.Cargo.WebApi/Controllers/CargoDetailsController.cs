using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DtoLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailsController : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService;

        public CargoDetailsController(ICargoDetailService cargoDetailService)
        {
            _cargoDetailService = cargoDetailService;
        }

        [HttpGet]
        public IActionResult CargoDetailList()
        {
            var values = _cargoDetailService.TGetAll();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetCargoDetailById(int id)
        {
            var values = _cargoDetailService.TGetById(id);
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateCargoDetail(CreateCargoDetailDto dto)
        {
            CargoDetail cargoDetail = new CargoDetail
            {
                SenderCustomer = dto.SenderCustomer,
                ReciverCustomer = dto.ReciverCustomer,
                Barcode = dto.Barcode,
                CargoCompanyId = dto.CargoCompanyId
            };
            _cargoDetailService.TInsert(cargoDetail);
            return Ok("Kargo detayı başarıyla eklendi");
        }

        [HttpPut]
        public IActionResult UpdateCargoDetail(UpdateCargoDetailDto dto)
        {
            CargoDetail cargoDetail = new CargoDetail
            {
                CargoDetailId = dto.CargoDetailId,
                SenderCustomer = dto.SenderCustomer,
                ReciverCustomer = dto.ReciverCustomer,
                Barcode = dto.Barcode,
                CargoCompanyId = dto.CargoCompanyId
            };
            _cargoDetailService.TUpdate(cargoDetail);
            return Ok("Kargo detayı güncelleme başarılı");
        }

        [HttpDelete]
        public IActionResult RemoveCargoDetail(int id)
        {
            _cargoDetailService.TDelete(id);
            return Ok("Kargo detayı başarıyla silindi");
        }
    }
}
