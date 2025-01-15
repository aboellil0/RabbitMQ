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

        [HttpPost]
        public async Task<IActionResult> CreateBoocking(Booking newBooking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _context.Bookings.AddAsync(newBooking);
            await _messageProduser.SendingMessages(newBooking);


            return Ok();
        }

    }
}
