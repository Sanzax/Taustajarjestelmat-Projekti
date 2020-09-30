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
        public Task<Player> CreatePlayer([FromBody] NewPlayer player)
        {



            return null;

        }
        [HttpPost]
        public Task<Player> ModifyPlayer(Guid id, [FromBody] ModifiedPlayer modifiedPlayer)
        {

            return null;

        }

        [HttpGet]
        public Task<Nationality[]> GetTopNationalities(int n)
        {

            return null;

        }


    }



}