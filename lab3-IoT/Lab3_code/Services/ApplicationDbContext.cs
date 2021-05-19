using System;
using Lab3_code.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab3_code.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> People{ get; set; }
    }
}
