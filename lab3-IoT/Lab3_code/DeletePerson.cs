using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Lab3_code.Models;
using Lab3_code.Services;
using System.Linq;

namespace Lab3_code
{
    public class DeletePerson
    {
        private readonly ApplicationDbContext _context;

        public DeletePerson(ApplicationDbContext context)
        {
            _context = context;
        }

        [FunctionName("DeletePerson")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                if (string.IsNullOrEmpty(req.Query["id"])) throw new Exception("Id must be given");
                int id = int.Parse(req.Query["id"]);
                var toDelete = _context.People.FirstOrDefault(x => x.PersonId == id);
                if(toDelete == null) throw new Exception("Cannot find person with given id");
                _context.People.Remove(toDelete);
                _context.SaveChanges();

                return new OkObjectResult(new
                {
                    Success = true,
                    People = new List<Person>(){toDelete}
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
