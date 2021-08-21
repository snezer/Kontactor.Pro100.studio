using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace KONTAKTOR.DA.Models
{
    public class Message : BaseModel
    {
        public string Text { get; set; }
        public DateTime SendDate { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string ChatId { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Attachment { get; set; }
    }
}
