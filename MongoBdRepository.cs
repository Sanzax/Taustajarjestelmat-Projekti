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
        await _sessionCollection.InsertOneAsync(session);
        
        return session;

    }


    public async Task<NationalityCount[]> GetTopNationalities(int n)
    {
        List<NationalityCount> natCounts = await _playerCollection.Aggregate().Project(p => (int)p.nationality)
              .Group(l => l, p => new NationalityCount { Id = p.Key, Count = p.Sum() })
              .SortByDescending(l => l.Count)
              .Limit(n)
              .ToListAsync();
        //Console.WriteLine("Debug: " + levelCounts.First());
        return natCounts.ToArray();



    }

    public async Task<float?> GetSessionMedianLength()
    {
        return MedianFromList(await GetListOfPropertyInSession(session => (float)session.LengthInSeconds));
    }

    public async Task<float?> GetSessionAverageLength()
    {
        return AverageFromList(await GetListOfPropertyInSession(session => (float)session.LengthInSeconds));
    }

    public async Task<float?> GetMedianStartsPerSession()
    {
        return MedianFromList(await GetListOfPropertyInSession(session => (float)session.Starts));
    }

    public async Task<float?> GetAverageStartsPerSession()
    {
        return AverageFromList(await GetListOfPropertyInSession(session => (float)session.Starts));
    }

    public async Task<float?> GetMedianDeathsPerSession()
    {
        return MedianFromList(await GetListOfPropertyInSession(session => (float)session.Deaths));
    }

    public async Task<float?> GetAverageDeathsPerSession()
    {
        return AverageFromList(await GetListOfPropertyInSession(session => (float)session.Deaths));
    }

    public async Task<float?> GetMedianWinsPerSession()
    {
        return MedianFromList(await GetListOfPropertyInSession(session => (float)session.Wins));
    }

    public async Task<float?> GetAverageWinsPerSession()
    {
        return AverageFromList(await GetListOfPropertyInSession(session => (float)session.Wins));
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
            sum += member;

        return sum / list.Count;
    }

    /*private async Task<List<float>> GetSessionLengths()
    {
        FilterDefinition<Session> filter = Builders<Session>.Filter.Empty;
        List<Session> sessions = await _sessionCollection.Find(filter).ToListAsync();

        List<float> sessionLengths = new List<float>();
        foreach(Session session in sessions)
        {
            sessionLengths.Add(session.LengthInSeconds);
        }
        return sessionLengths;
    }*/

    private async Task<List<T>> GetListOfPropertyInSession<T>(Func<Session, T> propertyOfSession)
    {
        FilterDefinition<Session> filter = Builders<Session>.Filter.Empty;
        List<Session> sessions = await _sessionCollection.Find(filter).ToListAsync();

        List<T> sessionProperties = new List<T>();
        foreach(Session session in sessions)
        {
            sessionProperties.Add(propertyOfSession(session));
        }
        return sessionProperties;
    }
}