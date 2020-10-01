using System;
using System.Net.Http;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Linq;
using System.IO;

public interface IRepository
{

    Task<Player> CreatePlayer(Player player);

    Task<Player> ModifyPlayer(string id, ModifiedPlayer modifiedPlayer);

    Task<Session> CreateSession(Session session);

    Task<NationalityCount[]> GetTopNationalities(int n);

    Task<WeeklyCount[]> GetWeeklyActivity();

    Task<DailyCount[]> GetDailyActivity();

    Task<float?> GetSessionMedianLength();

    Task<float?> GetSessionAverageLength();

    Task<float?> GetMedianStartsPerSession();

    Task<float?> GetAverageStartsPerSession();

    Task<float?> GetMedianDeathsPerSession();

    Task<float?> GetAverageDeathsPerSession();

    Task<float?> GetMedianWinsPerSession();

    Task<float?> GetAverageWinsPerSession();

    Task<GenderPercentage[]> GetGenderDistribution();

    Task<Player[]> GetAllPlayers();
    Task<Session[]> GetAllSessions();
    Task<int> GetPlayerCount();
    Task<int> GetSessionCount();
    Task<Player> GetPlayer(string id);
    Task<Session> GetSession(string id);
}