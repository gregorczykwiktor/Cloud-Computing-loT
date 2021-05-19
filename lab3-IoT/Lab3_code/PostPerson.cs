using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Lab3_code.Models;
using Lab3_code.Services;
using System.Collections.Generic;

namespace Lab3_code
{
    public class PostPerson
    {
        private readonly ApplicationDbContext _context;

        public PostPerson(ApplicationDbContext context)
        {
            _context = context;
        }
        [FunctionName("PostPerson")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {

                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic body = JsonConvert.DeserializeObject<PersonDTO>(requestBody);

                if (string.IsNullOrEmpty(body.Firstname) || string.IsNullOrEmpty(body.Lastname)) throw new Exception("Firstname and lastname must be provided");

                var toAdd = new Person
                {
                    Firstname = body.Firstname,
                    Lastname = body.Lastname
                };

                _context.People.Add(toAdd);

                _context.SaveChanges();
                return new OkObjectResult(new
                {
                    Success = true,
                    People = new List<Person>() { toAdd }
                });
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
    }
}
