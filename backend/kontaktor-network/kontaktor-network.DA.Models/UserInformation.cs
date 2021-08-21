using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace kontaktor_network.DA.Models
{
    public class UserInformation : BaseModel
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// Пользователь не может больше использовать систему
        /// </summary>
        public bool Disabled { get; set; }
        public AccountingInformation AccountingInformation { get; set; }
        public string[] SystemRoles { get; set; }
        /// <summary>
        /// Является ли арендатором как физическое лицо
        /// </summary>
        public bool IsTenant { get; set; }
        /// <summary>
        /// Обратная ссылка на сущность Арендатор, заполнена, если пользователь является арендатором-физиком
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string TenancyId { get; set; }
        /// <summary>
        /// Является ИП
        /// </summary>
        public bool IsIP { get; set; }
        
    }
}
