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
                Deaths = newSession.Deaths,
                Day = DateTime.Now.DayOfWeek,
                Hour = DateTime.Now.Hour

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
        [Route("WeeklyActivity")]
        public async Task<WeeklyCount[]> GetWeeklyActivity()
        {
            string[] days = await _repository.GetWeeklyActivity();
            WeeklyCount[] dayCount = new WeeklyCount[7];
            string separator = ",";
            int count = 2;

            for (int i = 0; i < days.Length; i++)
            {

                WeeklyCount day = new WeeklyCount();
                String[] tempString = new string[2];
                tempString = days[i].Split(separator, count, StringSplitOptions.RemoveEmptyEntries);

                day.Day = (DayOfWeek)Int32.Parse(tempString[0]);
                day.Name = day.Day.ToString();
                day.Count = Int32.Parse(tempString[1]);
                dayCount[i] = day;
            }
            return dayCount;
        }

        [HttpGet]
        [Route("DailyActivity")]
        public async Task<DailyCount[]> GetDailyActivity()
        {
            string[] hours = await _repository.GetDailyActivity();
            DailyCount[] hourCount = new DailyCount[24];
            string separator = ",";
            int count = 2;

            for (int i = 0; i < hours.Length; i++)
            {

                DailyCount hour = new DailyCount();
                String[] tempString = new string[2];
                tempString = hours[i].Split(separator, count, StringSplitOptions.RemoveEmptyEntries);

                hour.Hour = Int32.Parse(tempString[0]);

                hour.Count = Int32.Parse(tempString[1]);
                hourCount[i] = hour;
            }
            return hourCount;



        }
    }

}