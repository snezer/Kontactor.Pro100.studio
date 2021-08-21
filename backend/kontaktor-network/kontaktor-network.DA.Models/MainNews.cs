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
        /// <summary>
        /// Пользователь отметил новость как прочитанную/закрыл ее крестиком
        /// </summary>
        public bool Closed { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreationDate { get; set; }
        /// <summary>
        /// Дата, после которой новость должна быть опубликована
        /// </summary>
        public DateTime? PublishDate { get; set; }
    }
}
