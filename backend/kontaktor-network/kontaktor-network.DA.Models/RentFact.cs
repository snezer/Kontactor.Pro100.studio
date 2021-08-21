using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KONTRAKTOR.DA.Models
{
    public class RentFact : BaseModel
    {
        public bool IsValidated { get; set; }
        /// <summary>
        /// Ссылка на работника УК, подтвердившего бронирование
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string ValidatedByUser { get; set; }

        public DateTime ContractStart { get; set; }

        public DateTime? ContractEnd { get; set; }
        /// <summary>
        /// Арендатор
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string TenantId { get; set; }
        /// <summary>
        /// Арендуемое помещение
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompartmentId { get; set; }

        public DateTime? ContractSignDate { get; set; }
    }
}
