using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Order.Application.Features.CQRS.Commands.AdressCommands;
using MultiShop.Order.Application.Features.CQRS.Handlers.AdressHandlers;
using MultiShop.Order.Application.Features.CQRS.Queries.AdressQueries;

namespace MultiShop.Order.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdressesController : ControllerBase
    {
        private readonly GetAdressQueryHandler _getAdressQueryHandler;
        private readonly GetAdressByIdQueryHandler _getAdressByIdQueryHandler;
        private readonly CreateAdressCommandHandler _createAdressCommandHandler;
        private readonly UpdateAdressCommandHandler _updateAdressCommandHandler;
        private readonly RemoveAdressCommandHandler _deleteAdressCommandHandler;

        public AdressesController(GetAdressQueryHandler getAdressQueryHandler, GetAdressByIdQueryHandler getAdressByIdQueryHandler, CreateAdressCommandHandler createAdressCommandHandler, UpdateAdressCommandHandler updateAdressCommandHandler, RemoveAdressCommandHandler deleteAdressCommandHandler)
        {
            _getAdressByIdQueryHandler = getAdressByIdQueryHandler;
            _getAdressQueryHandler = getAdressQueryHandler;
            _createAdressCommandHandler = createAdressCommandHandler;
            _updateAdressCommandHandler = updateAdressCommandHandler;
            _deleteAdressCommandHandler = deleteAdressCommandHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdresses()
        {
            var result = await _getAdressQueryHandler.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AdressListById(int id)
        {
            var result = await _getAdressByIdQueryHandler.Handle(new GetAdressByIdQuery(id));
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdress(CreateAdressCommand command)
        {
            await _createAdressCommandHandler.Handle(command);
            return Ok("Adres bilgisi başarıyla eklendi");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdress(UpdateAdressCommand command)
        {
            await _updateAdressCommandHandler.Handle(command);
            return Ok("Adres bilgisi başarıyla güncellendi");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAdress(int id)
        {
            await _deleteAdressCommandHandler.Handle(new RemoveAdressCommand(id));
            return Ok("Adres bilgisi başarıyla silindi");
        }
    }
}
