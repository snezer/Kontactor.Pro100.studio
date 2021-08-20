using System;
using System.Collections.Generic;
using System.Text;
using KONTAKTOR.DA.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace kontaktor_network.DA.Models
{
    public class MainNews : BaseModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ContentId { get; set; }
    }
}
