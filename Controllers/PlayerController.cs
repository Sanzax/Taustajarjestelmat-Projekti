using System;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;


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
        [Route("GetTopNationalities/{n}")]
        public Task<NationalityCount[]> GetTopNationalities(int n)
        {

            return _repository.GetTopNationalities(n);

        }
        [HttpGet]
        [Route("GetGenderDistribution")]
        public async Task<GenderPercentage[]> GetGenderDistribution(){
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

    }
}