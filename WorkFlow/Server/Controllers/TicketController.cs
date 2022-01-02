using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WorkFlow.Shared.Dto;
using WorkFlow.Shared.Interfaces;

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
            TicketModel = ticketModel;
        }

        // GET: api/ticket
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<TicketDto> tickets = await TicketModel.List();
                return Ok(tickets);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // GET api/ticket/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                TicketDto tickets = await TicketModel.Get(id);
                return Ok(tickets);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // GET: api/ticket/project/{projectId}
        [HttpGet("project/{projectId:guid}")]
        public async Task<IActionResult> GetByProject(Guid projectId)
        {
            try
            {
                List<TicketDto> tickets = await TicketModel.ListTicketsByProject(projectId);
                return Ok(tickets);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // GET: api/ticket/user/{userId}
        [HttpGet("project/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            try
            {
                List<TicketDto> tickets = await TicketModel.ListTicketsByUser(userId);
                return Ok(tickets);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
        
        // POST api/ticket
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TicketDto ticket)
        {
            try
            {
                TicketDto newTicket = await TicketModel.Create(ticket);
                return Ok(newTicket);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT api/ticket/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] TicketDto ticket)
        {
            try
            {
                TicketDto tickets = await TicketModel.Update(id, ticket);
                return Ok(tickets);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE api/ticket/{id} 
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool deleted = await TicketModel.Delete(id);
                return Ok(deleted);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}