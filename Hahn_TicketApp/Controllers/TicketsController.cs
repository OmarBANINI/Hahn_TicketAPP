using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TicketApp.Exceptions;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var tickets = await _ticketService.GetTickets(pageNumber, pageSize);
                return Ok(tickets);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        // GET: api/Tickets/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicket(int id)
        {
            try
            {
                var ticket = await _ticketService.GetTicket(id);
                if (ticket == null) return NotFound();
                return Ok(ticket);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Tickets
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            try
            {
                await _ticketService.CreateTicket(ticket);
                return CreatedAtAction(nameof(GetTicket), new { id = ticket.TicketID }, ticket);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Tickets/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.TicketID) return BadRequest();

            try
            {
                await _ticketService.UpdateTicket(ticket);
                return NoContent();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            try
            {
                await _ticketService.DeleteTicket(id);
                return NoContent();
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
