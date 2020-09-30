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
        public Task<Session> CreateSession([FromBody] NewSession session)
        {

            return null;

        }
        [HttpGet]
        public Task<float?> GetSessionMedianLength()
        {

            return null;

        }
        [HttpGet]
        public Task<float?> GetSessionAverageLength()
        {

            return null;

        }
        [HttpGet]
        public Task<float?> GetMedianStartsPerSession()
        {

            return null;

        }
        [HttpGet]
        public Task<float?> GetAverageStartsPerSession()
        {

            return null;

        }
        [HttpGet]
        public Task<float?> GetMedianDeathsPerSession()
        {

            return null;

        }
        [HttpGet]
        public Task<float?> GetAverageDeathsPerSession()
        {

            return null;

        }
    }



}