using BirdClubAPI.BusinessLayer.Helpers;
using BirdClubAPI.DataAccessLayer.Context;
using BirdClubAPI.DataAccessLayer.Repositories.Activity;

namespace BirdClubAPI.BusinessLayer.Jobs
{
    public class RecurringJobs : IRecurringJobs
    {
        private readonly IActivityRepository _activityRepository;

        public RecurringJobs(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task CheckIncomingEvent()
        {
            var listEvent = _activityRepository.GetActivities().Where(e => e.StartTime.AddHours(-1) > DateTime.Now.AddMinutes(-15) && e.StartTime.AddHours(-1) < DateTime.Now.AddMinutes(15)).ToList();
            Console.WriteLine(listEvent.Count);
            foreach (var element in listEvent)
            {
                var attendances = _activityRepository.GetAttendance(element.Id);
                foreach (var attendee in attendances)
                {
                    await FirebaseHelper.Write(attendee.MemberId, new Notification
                    {
                        Title = "Activity",
                        Message = $"Activity {element.Name} has upcoming in 1 hour"
                    });
                }
            }
        }
    }
}
