using BasDatEv3.Entities;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BasDatEv3.DBActions
{
    internal class MongoConnection
    {
        private static IMongoClient _mongoClient;
        private static IMongoDatabase _database;
        private static List<IMongoCollection<BsonDocument>> _collections = new List<IMongoCollection<BsonDocument>>();
        public static IMongoClient MongoClient { get => _mongoClient; set => _mongoClient = value; }
        public static IMongoDatabase Database { get => _database; set => _database = value; }
        public static List<IMongoCollection<BsonDocument>> Collections { get => _collections; set => _collections = value; }

        public static void OpenConnection()
        {
            MongoClient = new MongoClient("mongodb://localhost:27017");
        }

        public static void CloseConnection()
        {
            MongoClient?.Cluster?.Dispose();
        }

        public static void DropIfExists(string dbName)
        {
            IAsyncCursor<string> databaseNames = _mongoClient.ListDatabaseNames();
            List<string> dbList = databaseNames.ToList();

            foreach (string name in dbList)
            {
                if (name == dbName)
                {
                    _mongoClient.DropDatabaseAsync(dbName);
                }
                else
                {
                    CreateDatabase(dbName);
                }
            }
        }

        public static void CreateDatabase(string dbName)
        {
            var server = MongoClient.GetDatabase("admin");

            var result = server.RunCommand(new JsonCommand<BsonDocument>("{ create: '" + dbName + "' }"));

            Database = MongoClient.GetDatabase(dbName);
        }

        public static void CreateCollection(string colName)
        {
            Database.CreateCollection(colName);
        }

        public static List<T> GetCollectionData<T>(string colName)
        {
            IMongoCollection<T> col = Database.GetCollection<T>(colName);

            var filter = Builders<T>.Filter.Empty; 
            var result = col.Find(filter).ToList();

            return result;
        }

        public static List<string> GetLocationsNames()
        {
            List<Location> data = GetCollectionData<Location>("Locations");

            List<string> result = data.Select(l => l.LocationCode).ToList();

            return result;
        }

        //INSERTS
        public static string InsertPiece(Piece piece)
        {
            string answer = string.Empty;

            if (string.IsNullOrEmpty(piece.Name) || string.IsNullOrEmpty(piece.Type) || piece.Cost == null || piece.Date == null)
            {
                answer = "Debe llenar todos los campos";
            }
            else
            {
                IMongoCollection<Piece> col = Database.GetCollection<Piece>("Pieces");
                col.InsertOne(piece);
            }

            return answer;
        }

        public static string InsertLocation(Location loc)
        {
            string answer = string.Empty;

            if (string.IsNullOrEmpty(loc.LocationCode))
            {
                answer = "Debe llenar todos los campos";
            }
            else
            {
                IMongoCollection<Location> col = Database.GetCollection<Location>("Locations");
                col.InsertOne(loc);
            }

            return answer;
        }

        //DELETS
        public static void DeleteLocation(string collName, Location data)
        {
            IMongoCollection<Location> col = Database.GetCollection<Location>(collName);
            IMongoCollection<Piece> colPiece = Database.GetCollection<Piece>("Pieces");

            var filterToUpdate = Builders<Piece>.Filter.Eq("locacion._id", ObjectId.Parse(data.Id));
            var update = Builders<Piece>.Update.Set("locacion", BsonNull.Value);
            colPiece.UpdateMany(filterToUpdate, update);

            var filterToDelete = Builders<Location>.Filter.Eq("_id", ObjectId.Parse(data.Id));
            col.DeleteOne(filterToDelete);
        }

        public static void DeletePiece(string collName, Piece data)
        {
            IMongoCollection<Location> col = Database.GetCollection<Location>(collName);
            var filterToDelete = Builders<Location>.Filter.Eq("_id", ObjectId.Parse(data.Id));

            col.DeleteOne(filterToDelete);
        }

        public static string UpdateLocation(Location olddata,Location newdata)
        {
            string answer = string.Empty;

            if (string.IsNullOrEmpty(newdata.LocationCode))
            {
                answer = "Debe llenar todos los campos";
            }
            else
            {
                IMongoCollection<Location> col = Database.GetCollection<Location>("Locations");
                IMongoCollection<Piece> colPiece = Database.GetCollection<Piece>("Pieces");

                var filterPieces = Builders<Piece>.Filter.Eq("locacion", olddata);
                var updatePieces = Builders<Piece>.Update.Set("locacion", newdata);
                colPiece.UpdateMany(filterPieces, updatePieces);


                var filterLocations = Builders<Location>.Filter.Eq("_id", ObjectId.Parse(olddata.Id));
                var updateLocation = Builders<Location>.Update.Set("locacion", newdata.LocationCode);
                col.UpdateOne(filterLocations, updateLocation);
            }

            return answer;
        }
    }
}
