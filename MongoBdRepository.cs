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

    public async Task<Player> CreatePlayer(Player player)
    {

        return null;
    }

    public async Task<Player> ModifyPlayer(Guid id, ModifiedPlayer modifiedPlayer)
    {

        return null;
    }

    public async Task<Session> CreateSession(Session session)
    {

        return null;
    }

    public async Task<Nationality[]> GetTopNationalities(int n)
    {

        return null;
    }


    public async Task<float?> GetSessionMedianLength()
    {

        return null;
    }

    public async Task<float?> GetSessionAvegageLenght()
    {

        return null;
    }


    public async Task<float?> GetMedianStartsPerSession()
    {


        return null;
    }

    public async Task<float?> GetAverageStartsPerSession()
    {


        return null;

    }

    public async Task<float?> GetMedianDeathsPerSession()
    {


        return null;

    }

    public async Task<float?> GetAverageDeathsPerSession()
    {


        return null;

    }

}