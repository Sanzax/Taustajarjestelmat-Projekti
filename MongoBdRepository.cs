using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Http;

public class MongoDBRepository : IRepository
{
    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<Session> _sessionCollection;
    private readonly IMongoCollection<BsonDocument> _bsonPlayerCollection;
    private readonly IMongoCollection<BsonDocument> _bsonSessionCollection;
    //  private ErrorHandlerMiddleware _middleware;

    public MongoDBRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
        _bsonPlayerCollection = database.GetCollection<BsonDocument>("players");
        _sessionCollection = database.GetCollection<Session>("sessions");
        _bsonSessionCollection = database.GetCollection<BsonDocument>("players");
    }

    public Task<Player> CreatePlayer(Player player)
    {

        return null;

    }

    public Task<Player> ModifyPlayer(Guid id, ModifiedPlayer modifiedPlayer)
    {

        return null;

    }


    public Task<Session> CreateSession(Session session)
    {

        return null;

    }


    public Task<Nationality[]> GetTopNationalities(int n)
    {

        return null;

    }

    public Task<float?> GetSessionMedianLength()
    {

        return null;

    }

    public Task<float?> GetSessionAverageLength()
    {

        return null;

    }

    public Task<float?> GetMedianStartsPerSession()
    {

        return null;

    }

    public Task<float?> GetAverageStartsPerSession()
    {

        return null;

    }

    public Task<float?> GetMedianDeathsPerSession()
    {

        return null;

    }

    public Task<float?> GetAverageDeathsPerSession()
    {

        return null;

    }

        private float MedianFromList(List<float> list)
        {
            list.Sort();

            int middle = list.Count / 2;

            if(list.Count % 2 != 0)
                return list[middle];

            float a = list[middle];
            float b = list[middle-1];
            return (a + b) / 2;
        }

        private float AverageFromList(List<float> list)
        {
            float sum = 0;
            foreach (float f in list)
                sum += f;

            return sum / list.Count;
        }

}