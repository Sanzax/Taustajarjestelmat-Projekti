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
        var database = mongoClient.GetDatabase("project");
        _playerCollection = database.GetCollection<Player>("players");
        _bsonPlayerCollection = database.GetCollection<BsonDocument>("players");
        _sessionCollection = database.GetCollection<Session>("sessions");
        _bsonSessionCollection = database.GetCollection<BsonDocument>("sessions");
    }

    public async Task<Player> CreatePlayer(Player player)
    {
        player.CreationDate = DateTime.Now;
        player.Id = Guid.NewGuid().ToString();
        await _playerCollection.InsertOneAsync(player);
        return player;
    }

    public async Task<Player> ModifyPlayer(string id, ModifiedPlayer modifiedPlayer)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        var update = Builders<Player>.Update.Set(p => p.Age, modifiedPlayer.age);
        await _playerCollection.UpdateOneAsync(filter, update);
        return null;

    }


    public async Task<Session> CreateSession(Session session)
    {
        session.EndTime = DateTime.Now;
        await _sessionCollection.InsertOneAsync(session);

        return session;

    }


    public async Task<string[]> GetTopNationalities(int n)
    {
        /*   List<NationalityCount> natCounts = await _playerCollection.Aggregate().Project(p => (int)p.Nationality)
                 .Group(l => l, p => new NationalityCount { nationality = (Nationality)p.Key, Count = p.Sum() })
                 .SortByDescending(l => l.Count)
                 .Limit(n)
                 .ToListAsync();

           return natCounts.ToArray();*/
        var dbResult = await _playerCollection.Aggregate()
        .Unwind(p => p.Nationality)
        .Group(e => e["Nationality"], n => new { Nationality = n.Key, Count = n.Count() })
        .SortByDescending(e => e.Count)
        .Limit(n)
        .ToListAsync();
        var result = dbResult.Select(t => t.Nationality.ToString() + "," + t.Count.ToString());
        // NationalityCount nc = dbResult.Select(nc.nationality => t.Nationality,)
        return result.ToArray();


    }


    public async Task<string[]> GetWeeklyActivity()
    {

        var dbResult = await _sessionCollection.Aggregate()
          .Unwind(s => s.Day)
          .Group(e => e["Day"], n => new { Day = n.Key, Count = n.Count() })
          .SortByDescending(e => e.Count)
          .Limit(7)
          .ToListAsync();
        var result = dbResult.Select(t => t.Day.ToString() + "," + t.Count.ToString());
        return result.ToArray();

    }

    public async Task<string[]> GetDailyActivity()
    {
        var dbResult = await _sessionCollection.Aggregate()
            .Unwind(s => s.Hour)
            .Group(e => e["Hour"], n => new { Hour = n.Key, Count = n.Count() })
            .SortByDescending(e => e.Count)
            .Limit(24)
            .ToListAsync();
        var result = dbResult.Select(t => t.Hour.ToString() + "," + t.Count.ToString());
        return result.ToArray();
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

    public async Task<float> GetMedianAge()
    {

        return MedianFromList(await GetAges());
    }

    public async Task<float> GetAverageAge()
    {
        return AverageFromList(await GetAges());

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
    private async Task<List<int>> GetAges()
    {

        Player[] players = await GetAllPlayers();
        int count = await GetPlayerCount();
        int[] ages = new int[count];
        for (int i = 0; i < count; i++)
        {
            ages[i] = players[i].Age;
        }
        return ages.ToList();
    }

    private async Task<List<T>> GetListOfPropertyInSession<T>(Func<Session, T> propertyOfSession)
    {
        FilterDefinition<Session> filter = Builders<Session>.Filter.Empty;
        List<Session> sessions = await _sessionCollection.Find(filter).ToListAsync();

        List<T> sessionProperties = new List<T>();
        foreach (Session session in sessions)
        {
            sessionProperties.Add(propertyOfSession(session));
        }
        return sessionProperties;
    }

    public async Task<GenderPercentage[]> GetGenderDistribution()
    {
        List<GenderPercentage> list = new List<GenderPercentage>();
        var m = (int)await _playerCollection.Find(p => p.Gender == 'M').CountDocumentsAsync();
        var f = (int)await _playerCollection.Find(f => f.Gender == 'F').CountDocumentsAsync();
        var o = (int)await _playerCollection.Find(f => f.Gender == 'O').CountDocumentsAsync();
        int total = m + f + o;
        list.Add(new GenderPercentage('M', (float)m / total));
        list.Add(new GenderPercentage('F', (float)f / total));
        list.Add(new GenderPercentage('O', (float)o / total));
        return list.ToArray();
    }
    public async Task<Player[]> GetAllPlayers()
    {
        return (await _playerCollection.Find(new BsonDocument()).ToListAsync()).ToArray();
    }

    public async Task<Session[]> GetAllSessions()
    {
        return (await _sessionCollection.Find(new BsonDocument()).ToListAsync()).ToArray();
    }

    public async Task<int> GetPlayerCount()
    {
        return (int)await _playerCollection.CountDocumentsAsync(new BsonDocument());
    }

    public async Task<int> GetSessionCount()
    {
        return (int)await _sessionCollection.CountDocumentsAsync(new BsonDocument());
    }

    public async Task<Player> GetPlayer(string id)
    {
        var filter = Builders<Player>.Filter.Eq(p => p.Id, id);
        return await _playerCollection.Find(filter).FirstAsync();
    }

    public async Task<Session> GetSession(string id)
    {
        var filter = Builders<Session>.Filter.Eq(p => p.id, id);
        return await _sessionCollection.Find(filter).FirstAsync();
    }


}