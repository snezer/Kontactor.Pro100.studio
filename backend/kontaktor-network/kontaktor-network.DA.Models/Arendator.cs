using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace kontaktor_network.DA.Models
{
    public class Arendator : BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string CompanyId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserInformationId { get; set; }
    }
}
