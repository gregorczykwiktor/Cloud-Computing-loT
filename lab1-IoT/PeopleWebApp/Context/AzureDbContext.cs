using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleWebApp.Models;

namespace PeopleWebApp.Context
{
        public class AzureDbContext : DbContext
        {
            public AzureDbContext(DbContextOptions<AzureDbContext> options) 
                : base(options)
            {

            }

            public DbSet<Person> People { get; set; }
        }
    
}
