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
using MongoDB.Driver.Linq;

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
        player.CreationDate = DateTime.Now;
        player.id = Guid.NewGuid();
        await _playerCollection.InsertOneAsync(player);
        return player;
    }

    public async Task<Player> ModifyPlayer(Guid id, ModifiedPlayer modifiedPlayer)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.id,id);
        var update = Builders<Player>.Update.Set(p =>p.age, modifiedPlayer.age);
        await _playerCollection.UpdateOneAsync(filter,update);
        return null;

    }


    public async Task<Session> CreateSession(Session session)
    {
        session.EndTime = DateTime.Now;
        TimeSpan dif = session.EndTime-session.StartTime;
        session.LenghtInSeconds = (int)dif.TotalSeconds;
        await _sessionCollection.InsertOneAsync(session);
        
        return session;

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

    private T MedianFromList<T>(List<T> list)
    {
        list.Sort();

        int middle = list.Count / 2;

        if (list.Count % 2 != 0)
            return list[middle];

        dynamic a = list[middle];
        dynamic b = list[middle - 1];
        return (a + b) / 2;
    }

    private T AverageFromList<T>(List<T> list)
    {
        dynamic sum = 0;
        foreach (dynamic member in list)
            sum = sum + member;

        return sum / list.Count;
    }

}