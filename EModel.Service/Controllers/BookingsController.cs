using AutoMapper;
using EModel.Contracts.Repository;
using EModel.Core.V1;
using EModel.Entities.DTOs;
using EModel.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EModel.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookingsController : ControllerBase
    {
        private readonly BookingCore _core;

        public BookingsController(
            IBookingRepository context, IBookingDetailRepository contextDetails, ILogger<Booking> logger,
            IMapper mapper, ILogger<BookingDetail> loggerDetails, IPaymentMethodRepository contextPayment, ILogger<PaymentMethod> loggerPayment,
            IUserRepository contextUser, ILogger<User> loggerUser
            )
        {
            _core = new(context, contextDetails, logger, mapper, loggerDetails, contextPayment, loggerPayment, contextUser, loggerUser);
        }

        // GET api/<PaymentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingShowDto>> GetBooking(int id)
        {
            var response = await _core.GetBooking(id);
            return StatusCode((int)response.StatusHttp, response);
        }

        // POST api/<PaymentsController>
        [HttpPost]
        public async Task<ActionResult<BookingShowDto>> NewBooking([FromBody] BookingCreateDto newBooking)
        {
            var response = await _core.NewBooking(newBooking);
            return StatusCode((int)response.StatusHttp, response);
        }
    }
}
