using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasDatEv3.Entities
{
    internal class Piece
    {
        private string _id;
        private string _name;
        private string _type;
        private Location _location;
        private double _cost;
        private int _stock;
        private DateTime _date;


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get => _id; set => _id = value; }

        [BsonElement("nombre")]
        public string Name { get => _name; set => _name = value; }

        [BsonElement("tipo")]
        public string Type { get => _type; set => _type = value; }

        [BsonElement("costo")]
        public double Cost { get => _cost; set => _cost = value; }

        [BsonElement("cantidad_en_stock")]
        public int Stock { get => _stock; set => _stock = value; }

        [BsonElement("fecha_de_registro")]
        public DateTime Date { get => _date; set => _date = value; }

        [BsonElement("locacion")]
        public Location Location { get => _location; set => _location = value; }
    }
}
