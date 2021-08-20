using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;

namespace kontaktor_network.DA.Models
{
    public class UserInformation : BaseModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public AccountingInformation AccountingInformation { get; set; }
        public string[] SystemRoles { get; set; }
    }
}
