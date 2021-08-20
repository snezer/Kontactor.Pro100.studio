using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using Dapper.Contrib.Extensions;
using kontaktor_network.DA.Models;


namespace KONTAKTOR.DA.Models
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AccountingInformation AccountingInformation { get; set; }
        public Occupation[] Occupations { get; set; }
    }

    public class Employee
    {
        public string UserId { get; set; }
        public string[] Roles { get; set; }
    }

    public class UserRole : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
