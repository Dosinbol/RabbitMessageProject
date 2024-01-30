using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMessageProjectAPI.Data;
using RabbitMessageProjectAPI.Models;
using RabbitMessageProjectAPI.Services.Abscract;

namespace RabbitMessageProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldiersController : ControllerBase
    {
        private readonly IProducer _producer;
        private MessageContext _context;
        public SoldiersController(IProducer producer, MessageContext context)
        {
            _producer = producer;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetSoldiers()
        {
            var allSoldiers = _context.Soldiers.ToList();
            if (allSoldiers == null)
            {
                return NotFound("Any of soldiers exist!");
            }
            return Ok(allSoldiers);
        }
        [HttpPost]
        public IActionResult CreatingSoldier(Soldier soldier)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Adding is failed!");
            }
            _context.Soldiers.Add(soldier);
            _context.SaveChanges();
            _producer.SendMessage(soldier);
            return Ok("Adding is successfull!");
        }
    }
}
