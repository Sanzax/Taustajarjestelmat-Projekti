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
        public async Task<Player> CreatePlayer([FromBody]Player player)
        {
            return await _repository.CreatePlayer(player);

        }
        [HttpPost]
        [Route("Modify/{id}")]
        public async Task<Player> ModifyPlayer(Guid id, [FromBody]ModifiedPlayer modifiedPlayer)
        {

            return await _repository.ModifyPlayer(id,modifiedPlayer);

        }

        [HttpPost]
        [Route("CreateSession")]
        public async Task<Session> CreateSession([FromBody]Session session)
        {

            return await _repository.CreateSession(session);

        }


        public Task<Nationality[]> GetTopNationalities(int n)
        {

            return _repository.GetTopNationalities(n);

        }

        public Task<float?> GetSessionMedianLength()
        {

            return _repository.GetSessionMedianLength();

        }

        public Task<float?> GetSessionAverageLength()
        {

            return _repository.GetSessionAverageLength();

        }

        public Task<float?> GetMedianStartsPerSession()
        {
        
            return _repository.GetMedianStartsPerSession();

        }

        public Task<float?> GetAverageStartsPerSession()
        {

            return _repository.GetAverageStartsPerSession();

        }

        public Task<float?> GetMedianDeathsPerSession()
        {
            return _repository.GetMedianDeathsPerSession();
        }

        public Task<float?> GetAverageDeathsPerSession()
        {

            return _repository.GetAverageDeathsPerSession();

        }

    }
}