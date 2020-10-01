using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Taustajarjestelmat_Projekti.Controllers
{
    [ApiController]
    [Route("sessions")]
    public class SessionController : ControllerBase
    {
        private readonly ILogger<SessionController> _logger;
        private readonly IRepository _repository;

        public SessionController(ILogger<SessionController> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Session> CreateSession([FromBody] NewSession newSession)
        {
            Session session = new Session()
            {
                id = Guid.NewGuid().ToString(),
                playerId = newSession.PlayerId,
                StartTime = DateTime.Now.AddSeconds(-newSession.LengthInSeconds),
                EndTime = DateTime.Now,
                Wins = newSession.Wins,
                Deaths = newSession.Deaths
            };

            return await _repository.CreateSession(session);
        }
        
        [HttpGet]
        [Route("MedianLength")]
        public async Task<float?> GetSessionMedianLength()
        {
            return await _repository.GetSessionMedianLength();
        }

        [HttpGet]
        [Route("AverageLength")]
        public async Task<float?> GetSessionAverageLength()
        {
            return await _repository.GetSessionAverageLength();
        }

        [HttpGet]
        [Route("MedianStarts")]
        public async Task<float?> GetMedianStartsPerSession()
        {
            return await _repository.GetMedianStartsPerSession();
        }

        [HttpGet]
        [Route("AverageStarts")]
        public async Task<float?> GetAverageStartsPerSession()
        {
            return await _repository.GetAverageStartsPerSession();
        }

        [HttpGet]
        [Route("MedianDeaths")]
        public async Task<float?> GetMedianDeathsPerSession()
        {
            return await _repository.GetMedianDeathsPerSession();
        }

        [HttpGet]
        [Route("AverageDeaths")]
        public async Task<float?> GetAverageDeathsPerSession()
        {
            return await _repository.GetAverageDeathsPerSession();
        }

        [HttpGet]
        [Route("MedianWins")]
        public async Task<float?> GetMedianWinsPerSession()
        {
            return await _repository.GetMedianWinsPerSession();
        }

        [HttpGet]
        [Route("AverageWins")]
        public async Task<float?> GetAverageWinsPerSession()
        {
            return await _repository.GetAverageWinsPerSession();
        }

        [HttpGet]
        [Route("GetAll")]
         public async Task<Session[]> GetAllSessions()
        {
            return await _repository.GetAllSessions();
        }
        [HttpGet]
        [Route("GetCount")]
         public async Task<int> GetPlayerCount()
         {
             return await _repository.GetSessionCount();
         }

         [HttpGet]
         [Route("Get/{id}")]
         public async Task<Session> GetSession(string id){
             return await _repository.GetSession(id);
         }
    }

}