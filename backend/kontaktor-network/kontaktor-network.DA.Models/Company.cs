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
        /// <summary>
        /// Полное наименование компании
        /// </summary>
        public string FullName { get; set; }
        public string Description { get; set; }
        public AccountingInformation AccountingInformation { get; set; }
        public Occupation[] Occupations { get; set; }
        /// <summary>
        /// Признак организации являющейся УК
        /// </summary>
        public bool IsMain { get; set; }
    }

    public class Employee : BaseModel
    {
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        public string CompanyId { get; set; }
        public string[] Roles { get; set; }
    }

    public class UserRole : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
