using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace kontaktor_network.DA.Models
{
    public class Tenant : BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserInformationId { get; set; }
        public Counter[] Counters { get; set; }
    }

    public class Counter
    {
        public string SerialNumber { get; set; }
        public string ResourceType { get; set; }

        public MonthlyValues Values { get; set; }
    }

    public class MonthlyValues
    {
        public DateTime EnterDate { get; set; }
        public decimal Volume { get; set; }
    }
}
