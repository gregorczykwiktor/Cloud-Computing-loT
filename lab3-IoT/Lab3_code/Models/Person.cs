using System;
namespace Lab3_code.Models
{

    public class PersonDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
    public class Person : PersonDTO
    {
        public int PersonId { get; set; }
    }
}
