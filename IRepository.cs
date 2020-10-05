using System;
using System.Net.Http;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public interface IRepository
{

    Task<Player> CreatePlayer(Player player);

    Task<Player> ModifyPlayer(string id, ModifiedPlayer modifiedPlayer);

    Task<Session> CreateSession(Session session);

    Task<string[]> GetTopNationalities(int n);

    Task<string[]> GetMostActivePlayers(int n);

    //Task<string[]> GetMostActiveNations(int n);

    Task<string[]> GetWeeklyActivity();

    Task<string[]> GetDailyActivity();

    Task<DateTime[]> GetDateTimes();
    Task<float?> GetSessionMedianLength();

    Task<float?> GetSessionAverageLength();

    Task<float?> GetMedianStartsPerSession();

    Task<float?> GetAverageStartsPerSession();

    Task<float?> GetMedianDeathsPerSession();

    Task<float?> GetAverageDeathsPerSession();

    Task<float?> GetMedianWinsPerSession();

    Task<float?> GetAverageWinsPerSession();

    Task<GenderPercentage[]> GetGenderDistribution();

    Task<float> GetMedianAge();

    Task<float> GetAverageAge();

    Task<Player[]> GetAllPlayers();
    Task<List<Nationality>> GetAllNations();
    Task<Session[]> GetAllSessions();
    Task<int> GetPlayerCount();
    Task<int> GetSessionCount();
    Task<Player> GetPlayer(string id);
    Task<Session> GetSession(string id);
    Task<AgePercentage[]> GetAgeDistribution();


}