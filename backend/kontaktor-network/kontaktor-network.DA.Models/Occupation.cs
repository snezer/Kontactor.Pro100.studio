using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;

namespace KONTAKTOR.DA.Models
{
    public class Occupation : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}
