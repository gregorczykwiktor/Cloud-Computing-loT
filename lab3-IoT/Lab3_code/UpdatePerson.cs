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
using System.Linq;
using Lab3_code.Models;
using System.Collections.Generic;

namespace Lab3_code
{
    public class UpdatePerson
    {
        private readonly ApplicationDbContext _context;

        public UpdatePerson(ApplicationDbContext context)
        {
            _context = context;
        }
        [FunctionName("UpdatePerson")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic body = JsonConvert.DeserializeObject<PersonDTO>(requestBody);
                
                if (string.IsNullOrEmpty(req.Query["id"])) throw new Exception("Id must be given");
                int id = int.Parse(req.Query["id"]);
                var toUpdate = _context.People.FirstOrDefault(x => x.PersonId == id);
                if (toUpdate == null) throw new Exception("Cannot find person with given id");

                if(!string.IsNullOrEmpty(body.Firstname)) toUpdate.Firstname = body.Firstname;
                if(!string.IsNullOrEmpty(body.Lastname)) toUpdate.Lastname = body.Lastname;

                _context.SaveChanges();

                return new OkObjectResult(new
                {
                    Success = true,
                    People = new List<Person>() { toUpdate }
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
