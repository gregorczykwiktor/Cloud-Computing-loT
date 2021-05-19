using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Lab3_code.Services;
using System.Collections.Generic;
using Lab3_code.Models;
using System.Linq;

namespace Lab3_code
{
    public class GetPerson
    {
        private readonly ApplicationDbContext _context;

        public GetPerson(ApplicationDbContext context)
        {
            _context = context;
        }
        [FunctionName("GetPeople")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var people = new List<Person>();
                if (!string.IsNullOrEmpty(req.Query["id"]))
                {
                    int id = int.Parse(req.Query["id"]);
                    people = _context.People.Where(x => x.PersonId == id).ToList();
                }
                else {
                    people = _context.People.ToList();

                }
                return new OkObjectResult(new
                {
                    Success = true,
                    People = people
                });
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
