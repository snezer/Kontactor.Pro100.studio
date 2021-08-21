using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Babel.Db.Models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
