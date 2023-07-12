using AutoMapper;
using AutoMapper.Execution;
using BirdClubAPI.BusinessLayer.Helpers;
using BirdClubAPI.DataAccessLayer.Repositories.Activity;
using BirdClubAPI.DataAccessLayer.Repositories.Comment;
using BirdClubAPI.DataAccessLayer.Repositories.Feedback;
using BirdClubAPI.DataAccessLayer.Repositories.Member;
using BirdClubAPI.DataAccessLayer.Repositories.User;
using BirdClubAPI.Domain.Commons.Constants;
using BirdClubAPI.Domain.Commons.Enums;
using BirdClubAPI.Domain.Commons.Utils;
using BirdClubAPI.Domain.DTOs.Request.Activity;
using BirdClubAPI.Domain.DTOs.Response.Activity;
using BirdClubAPI.Domain.DTOs.Response.Member;
using BirdClubAPI.Domain.DTOs.View.Acitivity;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.Entities;

namespace BirdClubAPI.BusinessLayer.Services.Activity
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IUserRepository _userRepository;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper, ICommentRepository commentRepository, IMemberRepository memberRepository, IFeedbackRepository feedbackRepository, IUserRepository userRepository)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
            _commentRepository = commentRepository;
            _memberRepository = memberRepository;
            _feedbackRepository = feedbackRepository;
            _userRepository = userRepository;
        }

        public KeyValuePair<MessageViewModel, AttendanceActivityViewModel?> AttendanceActivity(AttendanceActivityRequestModel requestModel)
        {
            var attendance = _mapper.Map<Domain.Entities.Attendance>(requestModel);
            attendance.AttendanceTime = DateTime.UtcNow;

            var result = _activityRepository.AttendanceActivity(attendance);
            if (result == null)
            {
                return new KeyValuePair<MessageViewModel, AttendanceActivityViewModel?>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
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

        public async Task<MessageViewModel> DeclineAttendance(int memberId, int activityId)
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

            var notification = new Notification
            {
                Title = "Activity",
                Message = $"Manager decline your request from activityId {activityId}"
            };
            await FirebaseHelper.Write(memberId, notification);

            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Declined request"
            };
        }

        public List<AcitivityViewModel> GetActivities(bool? isAll)
        {
            List<ActivityResponseModel> activities = _activityRepository.GetActivities(isAll);
            foreach ( var activityResponse in activities )
            {
                activityResponse.FeedbackCount = _feedbackRepository.GetFeedbacks(activityResponse.Id).Count;
            }
            return _mapper.Map<List<AcitivityViewModel>>(activities);
        }

        public KeyValuePair<MessageViewModel, List<AttendanceViewModel?>> GetAttendance(int id)
        {
            var activity = _activityRepository.GetActivity(id);

            if (activity == null)
            {
                return new KeyValuePair<MessageViewModel, List<AttendanceViewModel?>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "Activity not found"
                    },
                    new List<AttendanceViewModel?>()
                );
            }
            else
            {
                var response = _activityRepository.GetAttendance(id);
                if (response == null)
                {
                    return new KeyValuePair<MessageViewModel, List<AttendanceViewModel?>>(
                        new MessageViewModel
                        {
                            StatusCode = System.Net.HttpStatusCode.NotFound,
                            Message = "No members found"
                        },
                        new List<AttendanceViewModel?>()
                    );
                }
                return new KeyValuePair<MessageViewModel, List<AttendanceViewModel?>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = string.Empty
                    },
                    _mapper.Map<List<AttendanceViewModel?>>(response)
                );
            }
        }

        public List<AcitivityViewModel> GetActivitiesByOwner(int ownerId)
        {
            List<ActivityResponseModel> activities = _activityRepository.GetActivitiesByOwner(ownerId);
            foreach (var act in activities)
            {
                act.RequestCount = _activityRepository.GetAttendanceRequests(act.Id).Count;
            }
            return _mapper.Map<List<AcitivityViewModel>>(activities);
        }

        public List<ActivityCalenderViewModel> GetCalenderActivities()
        {
            List<ActivityCalenderViewModel> calenderActivities = new();
            List<ActivityResponseModel> activities = _activityRepository.GetActivities();
            foreach (var activity in activities)
            {
                List<DateTime> dateList = DateTimeManager.GetDatesInRange(activity.StartTime, activity.EndTime);
                foreach (var date in dateList)
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

        public async Task<MessageViewModel> PostAttendance(int memberId, int activityId)
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

            var notification = new Notification
            {
                Title = "Activity",
                Message = $"Manager approve your request attendance for activityId: {activityId}"
            };
            await FirebaseHelper.Write(memberId, notification);

            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Create attendance success"
            };
        }

        public async Task<MessageViewModel> RequestAttendance(int memberId, int activityId)
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

            var listManager = _userRepository.GetManagerAndAdmin();
            foreach (var manager in listManager)
            {
                var notification = new Notification
                {
                    Title = "Activity",
                    Message = $"There are new activity request for activityId: {activityId}"
                };
                await FirebaseHelper.Write(manager.Id, notification);
            }

            return new MessageViewModel
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Message = "Request created"
            };
        }
        public AttendanceStatusRm GetUserAttendanceStatus(int id, int memberId)
        {
            var activity = _activityRepository.GetActivitieWithAttendance(id);
            var isFeedback = _feedbackRepository.GetFeedbacks(id).AsQueryable().Any(e => e.OwnerId == memberId);
            if (activity == null) return new AttendanceStatusRm
            {
                Status = AttendanceStatusEnum.NOT_FOUND,
                Message = AttendanceStatusConstants.NOT_FOUND,
                IsFeedback = isFeedback
            };
            if (activity.EndTime.CompareTo(DateTime.UtcNow.AddHours(7)) < 0)
            {
                return new AttendanceStatusRm
                {
                    Status = AttendanceStatusEnum.CLOSED,
                    Message = AttendanceStatusConstants.CLOSED,
                    IsFeedback = isFeedback
                };
            }
            if (activity.AttendanceRequests.Any(e => e.MemberId == memberId))
            {
                return new AttendanceStatusRm
                {
                    Status = AttendanceStatusEnum.PENDING,
                    Message = AttendanceStatusConstants.PENDING,
                    IsFeedback = isFeedback
                };
            }
            else if (activity.Attendances.Any(e => e.MemberId == memberId))
            {
                return new AttendanceStatusRm
                {
                    Status = AttendanceStatusEnum.ACCEPTED,
                    Message = AttendanceStatusConstants.ACCEPTED,
                    IsFeedback = isFeedback
                };
            }
            else
            {
                return new AttendanceStatusRm
                {
                    Status = AttendanceStatusEnum.NOT_ATTEND,
                    Message = AttendanceStatusConstants.NOT_ATTEND,
                    IsFeedback = isFeedback
                };
            }
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
            if (result == false)
            {
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

            foreach (var comment in activity.Comments)
            {
                comment.Owner = _mapper.Map<MemberResponseModel>(_memberRepository.GetMember(comment.OwnerId));
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

        public KeyValuePair<MessageViewModel, List<AttendanceRequestRm>> GetAttendanceRequests(int id)
        {
            var activity = _activityRepository.GetActivity(id);

            if (activity == null)
            {
                return new KeyValuePair<MessageViewModel, List<AttendanceRequestRm>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "Activity not found"
                    },
                    new List<AttendanceRequestRm>()
                );
            }
            else
            {
                var response = _activityRepository.GetAttendanceRequests(id);
                return new KeyValuePair<MessageViewModel, List<AttendanceRequestRm>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Message = string.Empty
                    },
                    _mapper.Map<List<AttendanceRequestRm>>(response)
                    );
            }
        }

        public List<ActivityCalenderViewModel> GetCalenderActivitiesByMember(int memberId)
        {
            List<ActivityCalenderViewModel> calenderActivities = new();
            List<ActivityResponseModel> activities = _activityRepository.GetActivities()
                .Where(e => e.Owner.MemberId == memberId || _activityRepository.GetActivitieWithAttendance(e.Id)?.Attendances.FirstOrDefault(m => m.MemberId == memberId) != null).ToList();
            foreach (var activity in activities)
            {
                List<DateTime> dateList = DateTimeManager.GetDatesInRange(activity.StartTime, activity.EndTime);
                foreach (var date in dateList)
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

        public async Task<bool> PostComment(int id, ActivityCommentRequest request)
        {
            var comment = new Comment
            {
                OwnerId = request.OwnerId,
                Content = request.Content,
                Type = "ACTIVITY",
                ReferenceId = id,
                PublicationTime = DateTime.UtcNow.AddHours(7),
            };
            var result = _commentRepository.Create(comment) != null;

            var owner = _activityRepository.GetActivity(id)!;
            var notification = new Notification
            {
                Title = "Activity",
                Message = $"MemberId {request.OwnerId} has comment your activity"
            };
            await FirebaseHelper.Write(owner.OwnerId, notification);

            return result;
        }
    }
}
