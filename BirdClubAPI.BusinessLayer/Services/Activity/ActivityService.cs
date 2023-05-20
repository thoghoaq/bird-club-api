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

        public KeyValuePair<MessageViewModel, AcitivityCreateViewModel?> CreateActivity(CreateActivityRequestModel requestModel)
        {
            var activity = _mapper.Map<Domain.Entities.Activity>(requestModel);
            activity.CreateTime = DateTime.UtcNow.AddHours(7);

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

        public List<AcitivityViewModel> GetActivities()
        {
            List<ActivityResponseModel> activities = _activityRepository.GetActivities();
            return _mapper.Map<List<AcitivityViewModel>>(activities);
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
