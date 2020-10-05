using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Taustajarjestelmat_Projekti.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayersController : ControllerBase
    {
        private readonly ILogger<PlayersController> _logger;
        private readonly IRepository _repository;

        public PlayersController(ILogger<PlayersController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Player> CreatePlayer([FromBody] NewPlayer newPlayer)
        {
            DateTime birthDate = new DateTime(newPlayer.Year, newPlayer.Month, newPlayer.Day);
            if((DateTime.Now - birthDate).TotalSeconds <  0)
            {
                //Syntymäpäivä oli tulevaisuudesta
                //Heitetäänkö jonkilainen error?
            }

            Player player = new Player()
            {
                Nationality = newPlayer.Nationality,
                Id = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                BirthDate = birthDate,
                Gender = newPlayer.Gender 
            };

            return await _repository.CreatePlayer(player);
        }

        [HttpPost]
        [Route("Modify/{id}")]
        public async Task<Player> ModifyPlayer(string id, [FromBody] ModifiedPlayer modifiedPlayer)
        {

            return await _repository.ModifyPlayer(id, modifiedPlayer);

        }
        [HttpGet]
        [Route("GetAllNationalities")]
        public async Task<List<string>> GetallNationalities()
        {
            List<string> stringList = new List<string>();
            var list = await _repository.GetAllNations();
            foreach (var a in list)
            {
                stringList.Add(a.ToString());
            }
            return stringList;
        }
        [HttpGet]
        [Route("GetTopNationalities/{n}")]
        public async Task<NationalityCount[]> GetTopNationalities(int n)
        {
            string[] nationalities = await _repository.GetTopNationalities(n);
            NationalityCount[] natCount = new NationalityCount[n];
            string separator = ",";
            int count = 2;

            for (int i = 0; i < n; i++)
            {
                NationalityCount nation = new NationalityCount();
                String[] tempStrings = new string[2];
                tempStrings = nationalities[i].Split(separator, count, StringSplitOptions.RemoveEmptyEntries);

                nation.nationality = (Nationality)Int32.Parse(tempStrings[0]);
                nation.Name = nation.nationality.ToString();
                if (tempStrings.Length == 1)
                    nation.Count = 0;
                nation.Count = Int32.Parse(tempStrings[1]);
                natCount[i] = nation;
            }
            return natCount;

        }

        [HttpGet]
        [Route("GetMostActive/{n}")]
        public async Task<PlayerActivityCount[]> GetMostActivePlayers(int n)
        {
            string[] players = await _repository.GetMostActivePlayers(n);

            PlayerActivityCount[] activePlayers = new PlayerActivityCount[n];
            string separator = ",";
            int count = 2;
            for (int i = 0; i < players.Length; i++)
            {
                PlayerActivityCount player = new PlayerActivityCount();
                String[] tempStrings = new string[2];
                tempStrings = players[i].Split(separator, count);

                player.Player = await GetPlayer(tempStrings[0]);
                if (tempStrings.Length == 1)
                    player.Sessions = 0;
                player.Sessions = Int32.Parse(tempStrings[1]);

                activePlayers[i] = player;
            }

            return activePlayers;
        }

        [HttpGet]
        [Route("GetMostActiveNations/{n}")]
        public async Task<NationalActivity[]> GetMostActiveNations(int n)
        {
            int c = await GetPlayerCount();
            // var na = await GetallNationalities();
            // int nn = na.Count();
            PlayerActivityCount[] players = await GetMostActivePlayers(c);
            //  List<NationalActivity> natA = new List<NationalActivity>();
            List<NationalActivity> natActivity = new List<NationalActivity>();
            List<int> checkList = new List<int>();
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == null)
                    continue;
                if (!checkList.Contains((int)players[i].Player.Nationality))
                {
                    NationalActivity natAt = new NationalActivity();
                    natAt.Nation = players[i].Player.Nationality;
                    natAt.Count = players[i].Sessions;
                    natAt.Name = players[i].Player.Nationality.ToString();
                    checkList.Add((int)natAt.Nation);
                    natActivity.Add(natAt);
                }
                else
                {
                    int index = checkList.FindIndex(n => n == (int)players[i].Player.Nationality);
                    natActivity[index].Count += players[i].Sessions;
                }

            }
            var result = natActivity.OrderByDescending(n => n.Count).Take(n).ToArray();

            return result;
        }

        [HttpGet]
        [Route("GetGenderDistribution")]
        public async Task<GenderPercentage[]> GetGenderDistribution()
        {
            return await _repository.GetGenderDistribution();
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<Player[]> GetAllPlayers()
        {
            return await _repository.GetAllPlayers();
        }
        [HttpGet]
        [Route("GetCount")]
        public async Task<int> GetPlayerCount()
        {
            return await _repository.GetPlayerCount();
        }
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<Player> GetPlayer(string id)
        {
            return await _repository.GetPlayer(id);
        }

        [HttpGet]
        [Route("GetAvgAge")]
        public async Task<float> GetAverageAge()
        {


            return await _repository.GetAverageAge();
        }
        [HttpGet]
        [Route("GetMedAge")]
        public async Task<float> GetMedianAge()
        {


            return await _repository.GetMedianAge();
        }
        [HttpGet]
        [Route("GetAgeDistribution")]
        public async Task<AgePercentage[]> GetAgePercentages()
        {
            return await _repository.GetAgeDistribution();
        }

    }
}