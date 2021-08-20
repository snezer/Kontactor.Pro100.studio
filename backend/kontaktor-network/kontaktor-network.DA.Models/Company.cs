using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;


namespace KONTAKTOR.DA.Models
{
    public class Company : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Occupation[] Occupations { get; set; }
    }
}
