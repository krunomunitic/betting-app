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
            var ticketId = _ticketService.CreateTicket(ticket);

            return Ok(ticketId);
        }
    }
}
