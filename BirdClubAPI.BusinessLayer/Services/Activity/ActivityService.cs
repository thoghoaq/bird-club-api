using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.Activity;
using BirdClubAPI.Domain.Commons.Utils;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Common;

namespace BirdClubAPI.BusinessLayer.Services.Activity
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        public KeyValuePair<MessageViewModel, AttendanceActivityViewModel?> AttendanceActivity(AttendanceActivityRequestModel requestModel)
        {
            var attendance = _mapper.Map<Domain.Entities.Attendance>(requestModel);
            attendance.AttendanceTime = DateTime.UtcNow;

            var result = _activityRepository.AttendanceActivity(attendance);
            if(result == null)
            {
                return new KeyValuePair<MessageViewModel, AttendanceActivityViewModel?>(
                    new MessageViewModel
                    {
                        StatusCode  = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Error when attendance this activity"
                    }, null 
                    );
            }
            return new KeyValuePair<MessageViewModel, AttendanceActivityViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Message = "Welcome to Activity"
                },
                _mapper.Map<AttendanceActivityViewModel>(result)
                );
        }

        public KeyValuePair<MessageViewModel, AcitivityCreateViewModel?> CreateActivity(CreateActivityRequestModel requestModel)
        {
            var activity = _mapper.Map<Domain.Entities.Activity>(requestModel);
            activity.CreateTime = DateTime.UtcNow.AddHours(7);
            activity.Status = true;

            var result = _activityRepository.CreateActivity(activity);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, AcitivityCreateViewModel?>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        Message = "Error occurs when insert this activity"
                    }, null
                    );
            }

            return new KeyValuePair<MessageViewModel, AcitivityCreateViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Created,
                    Message = string.Empty
                },
                _mapper.Map<AcitivityCreateViewModel>(result)
                );
        }

        public MessageViewModel DeclineAttendance(int memberId, int activityId)
        {
            var alreadyRequest = _activityRepository.GetAttendanceRequest(memberId, activityId);
            if (alreadyRequest == null) return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Not found this request"
            };
            bool result = _activityRepository.DeleteAttendanceRequest(alreadyRequest);
            if (result == false) return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Error when decline request"
            };
            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Declined request"
            };
        }

        public List<AcitivityViewModel> GetActivities()
        {
            List<ActivityResponseModel> activities = _activityRepository.GetActivities();
            return _mapper.Map<List<AcitivityViewModel>>(activities);
        }

        public List<AcitivityViewModel> GetActivitiesByOwner(int ownerId)
        {
            List<ActivityResponseModel> activities = _activityRepository.GetActivitiesByOwner(ownerId);
            return _mapper.Map<List<AcitivityViewModel>>(activities);
        }

        public List<ActivityCalenderViewModel> GetCalenderActivities()
        {
            List<ActivityCalenderViewModel> calenderActivities = new();
            List<ActivityResponseModel> activities = _activityRepository.GetActivities();
            foreach (var activity in activities)
            {
                List<DateTime> dateList = DateTimeManager.GetDatesInRange(activity.StartTime, activity.EndTime);
                foreach(var date in dateList)
                {
                    calenderActivities.Add(new ActivityCalenderViewModel
                    {
                        Id = activity.Id,
                        Name = activity.Name,
                        ActivityType = activity.ActivityType,
                        CreateTime = activity.CreateTime,
                        Location = activity.Location,
                        Date = date,
                        StartTime = activity.StartTime.TimeOfDay.ToString(@"hh\:mm\:ss"),
                        EndTime = activity.EndTime.TimeOfDay.ToString(@"hh\:mm\:ss"),
                    });
                }
            }
            return calenderActivities.OrderBy(e => e.Date).ToList();
        }

        public MessageViewModel PostAttendance(int memberId, int activityId)
        {
            var alreadyRequest = _activityRepository.GetAttendanceRequest(memberId, activityId);
            if (alreadyRequest == null) return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Not found this request"
            };
            bool isRemoved = _activityRepository.RemoveAttendanceRequest(alreadyRequest);
            if (isRemoved == false) return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Error when remove request"
            };
            var attendance = _activityRepository.PostAttendance(memberId, activityId);
            if (attendance == null) return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Error when create attendance"
            };
            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Create attendance success"
            };
        }

        public MessageViewModel RequestAttendance(int memberId, int activityId)
        {
            var alreadyRequest = _activityRepository.GetAttendanceRequest(memberId, activityId);
            if (alreadyRequest != null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Request has already existed"
                };
            }
            var request = _activityRepository.PostAttendanceRequest(memberId, activityId);
            if (request == null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Message = "Create request fail"
                };
            }
            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Request created"
            };
        }

        public MessageViewModel UpdateActivity(int id, UpdateActivityRequestModel requestModel)
        {
            var activity = _activityRepository.GetActivities(id);
            if (activity == null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This activity is not exist or deactivated"
                };
            }
            activity = ExcludeNullPropertiesMapper.Map<UpdateActivityRequestModel>(requestModel, activity);
            var result = _activityRepository.UpdateActivity(activity);
            if (result == false) {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    Message = "An error occurs when update this activity"
                };
            }
            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Update activity successful"
            };
        }

        public MessageViewModel UpdateActivityStatus(int id, UpdateActivityStatusRequestModel requestModel)
        {
            var activity = _activityRepository.GetActivities(id);
            if (activity == null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = "This activity is not exist or deactivated"
                };
            }
            activity = ExcludeNullPropertiesMapper.Map<UpdateActivityStatusRequestModel>(requestModel, activity);
            var result = _activityRepository.UpdateActivity(activity);
            if (result == false)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.Conflict,
                    Message = "An error occurs when update status of this activity"
                };
            }
            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.NoContent,
                Message = "Update activity status successful"
            };
        }

        

        KeyValuePair<MessageViewModel, AcitivityViewModel?> IActivityService.GetActivities(int id)
        {
            var activity = _mapper.Map<ActivityResponseModel>(_activityRepository.GetActivities(id));
            if (activity == null)
            {
                return new KeyValuePair<MessageViewModel, AcitivityViewModel?>
                (
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "Not found this activity"
                    },
                    null
                );
            }
            var response = _mapper.Map<AcitivityViewModel>(activity);
            return new KeyValuePair<MessageViewModel, AcitivityViewModel?>(
                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                response
                );
        }
    }
}
