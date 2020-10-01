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

        public Task<NationalityCount[]> GetTopNationalities(int n)
        {

            return _repository.GetTopNationalities(n);

        }

    }
}