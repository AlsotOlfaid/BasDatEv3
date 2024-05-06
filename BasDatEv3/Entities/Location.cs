using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasDatEv3.Entities
{
    class Location
    {
        private string _id;
        private string _locationCode;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("locacion")]
        public string LocationCode { get => _locationCode; set => _locationCode = value; }
        
    }
}
