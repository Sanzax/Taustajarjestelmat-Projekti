using System;
using System.Net.Http;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using System.Linq;
using System.IO;

public interface IRepository
{

    Task<Player> CreatePlayer(Player player);

    Task<Player> ModifyPlayer(Guid id, ModifiedPlayer modifiedPlayer);

    Task<Session> CreateSession(Session session);

    Task<NationalityCount[]> GetTopNationalities(int n);

    Task<float?> GetSessionMedianLength();

    Task<float?> GetSessionAverageLength();

    Task<float?> GetMedianStartsPerSession();

    Task<float?> GetAverageStartsPerSession();

    Task<float?> GetMedianDeathsPerSession();

    Task<float?> GetAverageDeathsPerSession();

}