using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Models;
using RabbitMQ.Services;

namespace RabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoockingsController : ControllerBase
    {
        private readonly ILogger<BoockingsController> _logger;
        private readonly IMessageProduser _messageProduser;
        private readonly RabbitMQContext _context;

        public BoockingsController(ILogger<BoockingsController> logger,IMessageProduser messageProduser,RabbitMQContext context)
        {
            this._logger = logger;
            this._messageProduser = messageProduser;
            this._context = context;
        }

        [HttpPost("/CreateBoocking")]
        public async Task<IActionResult> CreateBoocking([FromBody] Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var BookDbItem = await _context.Bookings.AddAsync(new Booking
            {
                PassengerName = newBooking.PassengerName,
                PassportNb = newBooking.PassportNb,
                RFrom = newBooking.RFrom,
                RTo = newBooking.RTo,
                Status = newBooking.Status,
            });
            await _context.SaveChangesAsync();
            await _messageProduser.SendingMessages(newBooking);

            return Ok(new { id = BookDbItem.Entity.id });
        }

        [HttpPost("/BookDb")]
        public IActionResult BookDb([FromBody] Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            _context.Bookings.Add(newBooking);

            return Ok(newBooking);
        }
    }
}
