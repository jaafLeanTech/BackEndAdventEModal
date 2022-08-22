using AutoMapper;
using EModel.Contracts.Repository;
using EModel.Core.V1;
using EModel.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EModel.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentCore _core;
        public PaymentsController(IPaymentMethodRepository context, ILogger<PaymentMethod> logger, IMapper mapper)
        {
            _core = new(context, mapper, logger);
        }


        // GET api/<PaymentsController>/5
        [HttpGet("Methods/{idUser}")]
        public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethods(int idUser)
        {
            var response = await _core.GetAllMethods(idUser);
            return StatusCode((int)response.StatusHttp, response);
        }
    }
}
