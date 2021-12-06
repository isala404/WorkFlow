using Microsoft.AspNetCore.Mvc;
using WorkFlow.Shared.Entities;
using WorkFlow.Shared.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkFlow.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        [Microsoft.AspNetCore.Components.Inject]
        protected ITicket TicketModel { get; set; }

        public TicketController(ITicket ticketModel)
        {
            this.TicketModel = ticketModel;
        }

        // GET: api/<TicketController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tickets = await TicketModel.List(new User());
            return Ok(tickets);
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tickets = await TicketModel.GetTicket(id);
            if (tickets == null) return BadRequest("Invalid Ticket ID");
            return Ok(tickets);
        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Ticket ticket)
        {
            var newTicket = await TicketModel.CreateTicket(ticket);
            return Ok(newTicket);
        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Ticket ticket)
        {
            var updatedTicket = await TicketModel.UpdateTicket(id, ticket);
            if (updatedTicket == null)
                return BadRequest("Invalid Ticket ID");
            return Ok(updatedTicket);
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deleted = await TicketModel.DeleteTicket(id);
            return Ok(deleted);
        }
    }
}
