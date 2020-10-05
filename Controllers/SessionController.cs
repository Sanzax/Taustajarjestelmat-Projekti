using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
                //   Day = DateTime.Now.DayOfWeek,
                Hour = DateTime.Now.Hour

            };
            await _repository.UpdateSessionCount(newSession.PlayerId);
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
        [Route("DateTimes")]
        public async Task<DateTime[]> Datetimes()
        {

            return await _repository.GetDateTimes();
        }

        [HttpGet]
        [Route("WeeklyActivity")]
        public async Task<WeeklyCount[]> GetWeeklyActivity()
        {
            DateTime[] sessionsTimes = await Datetimes();

            WeeklyCount[] week = new WeeklyCount[7];

            for (int i = 0; i < 7; i++)
            {
                WeeklyCount day = new WeeklyCount();
                day.Day = (DayOfWeek)i;
                day.Name = day.Day.ToString();
                day.Count = 0;
                week[i] = day;
            }

            foreach (DateTime d in sessionsTimes)
            {
                int day = (int)d.DayOfWeek;
                week[day].Count = week[day].Count + 1;

            }

            week = week.OrderByDescending(day => day.Count).ToArray();
            return week;

        }

        [HttpGet]
        [Route("DailyActivity")]
        public async Task<DailyCount[]> GetDailyActivity()
        {

            DateTime[] sessionsTimes = await Datetimes();

            DailyCount[] day = new DailyCount[24];

            for (int i = 0; i < 24; i++)
            {
                DailyCount hour = new DailyCount();
                hour.Hour = i;
                hour.Count = 0;
                day[i] = hour;

            }

            foreach (DateTime d in sessionsTimes)
            {

                int hour = d.Hour;
                day[hour].Count += 1;


            }

            day = day.OrderByDescending(day => day.Count).ToArray();
            return day;






        }
    }

}