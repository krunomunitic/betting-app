using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BettingApp.Services;
using BettingApp.Models;

namespace BettingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var tickets = _ticketService.GetTickets();

            return Ok(tickets);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_ticketService.ValidateTicketBets(ticket.Bets))
            {
                return BadRequest();
            }

            if (!_ticketService.ValidateTicketStake(ticket.Stake))
            {
                return BadRequest();
            }

            _ticketService.CreateTicket(ticket);

            return Ok();
        }
    }
}
