using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using DataStore.EF;
using Microsoft.EntityFrameworkCore;

namespace TicketPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController: ControllerBase
    {
        private readonly BugsContext db;
        public TicketsController(BugsContext _db)
        {
            this.db = _db;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Tickets.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }
        [HttpPost]
        [Route("/api/tickets")]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
            return CreatedAtAction(nameof(GetById),
                    new {id=ticket.TicketId},
                    ticket
                );
        }

        [HttpPut]
        public IActionResult Put(int id,[FromBody] Ticket ticket)
        {
            if (id != ticket.TicketId) return BadRequest();
            db.Entry(ticket).State = EntityState.Modified;

            try
            { 
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (db.Tickets.Find(id) == null)
                    return NotFound();
                throw;
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var ticket = db.Tickets.Find(id);

            if (ticket == null) NotFound();

            db.Tickets.Remove(ticket);
            db.SaveChanges();

            return Ok(ticket);
        }
    }
}
