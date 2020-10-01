using System;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
            Player player = new Player()
            {
                Nationality = newPlayer.Nationality,
                Id = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                BirthDate = new DateTime(newPlayer.Year, newPlayer.Month, newPlayer.Day),
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
                String[] tempString = new string[2];
                tempString = nationalities[i].Split(separator, count, StringSplitOptions.RemoveEmptyEntries);

                nation.nationality = (Nationality)Int32.Parse(tempString[0]);
                nation.Name = nation.nationality.ToString();
                nation.Count = Int32.Parse(tempString[1]);
                natCount[i] = nation;
            }
            return natCount;

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

    }
}