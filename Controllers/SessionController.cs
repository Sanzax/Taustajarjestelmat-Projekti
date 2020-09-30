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

    }



}