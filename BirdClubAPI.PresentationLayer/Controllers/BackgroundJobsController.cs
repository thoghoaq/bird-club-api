using BirdClubAPI.BusinessLayer.Jobs;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace BirdClubAPI.PresentationLayer.Controllers
{
    public class BackgroundJobsController : ControllerBase
    {
        private readonly IRecurringJobs _recurringJobs;

        public BackgroundJobsController(IRecurringJobs recurringJobs)
        {
            _recurringJobs = recurringJobs;
        }

        [HttpGet("notify-upcoming-activity")]
        public IActionResult SendNotiUpcomingActivity()
        {
            #region Hangfire RecurringJobs
            RecurringJob.AddOrUpdate("Check UpComming Event", () => _recurringJobs.CheckIncomingEvent(), "*/15 * * * *");
            #endregion Hangfire RecurringJobs
            return Ok();
        }
    }
}
