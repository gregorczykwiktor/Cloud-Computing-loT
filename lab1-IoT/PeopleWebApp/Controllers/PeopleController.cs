using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleWebApp.Models;

namespace PeopleWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly Context.AzureDbContext db;

        public PeopleController(Context.AzureDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var people = db.People.ToList();

            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var people = await db.People.FindAsync(id);

            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpPost]
        public async Task<ActionResult> Create([Bind("FirstName", "LastName")] Person person)
        {
            db.Add(person);
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,Firstname,Lastname")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(person);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return Ok(person);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var people = await db.People.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            db.People.Remove(people);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(long id) =>
        db.People.Any(e => e.PersonId == id);
    }
}
