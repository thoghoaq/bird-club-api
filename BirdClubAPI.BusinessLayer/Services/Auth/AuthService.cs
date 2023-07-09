using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.User;
using BirdClubAPI.Domain.Commons.Utils;
using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Common;
using BirdClubAPI.Domain.DTOs.View.Member;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace BirdClubAPI.BusinessLayer.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public MessageViewModel ApproveMember(int id)
        {
            var approve = _userRepository.ApproveMember(id);
            if (approve == null)
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    Message = string.Empty
                };
            }
            else
            {
                return new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                };
            }

        }

        public KeyValuePair<MessageViewModel, List<GuestViewModel?>> GetListGuest()
        {
            var guest = _userRepository.GetListGuest();
            if (guest == null)
            {
                return new KeyValuePair<MessageViewModel, List<GuestViewModel?>>(
                    new MessageViewModel
                    {
                        StatusCode = System.Net.HttpStatusCode.NotFound,
                        Message = "There are no any guest"
                    },
                    new List<GuestViewModel?>()
                    );
            }
            return new KeyValuePair<MessageViewModel, List<GuestViewModel?>>(

                new MessageViewModel
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Message = string.Empty
                },
                _mapper.Map<List<GuestViewModel?>>(guest)
                );
        }

        public KeyValuePair<MessageViewModel, AuthViewModel?> Login(LoginFormRequestModel loginFormRequest)
        {
            var user = _userRepository.Get(loginFormRequest.Email, loginFormRequest.Password);
            if (user == null)
            {
                return new KeyValuePair<MessageViewModel, AuthViewModel?>(
                new MessageViewModel { StatusCode = HttpStatusCode.Unauthorized, Message = "Wrong user name or password" },
                null
                );
            }
            if (user.Member != null && user.Member.MembershipStatus == false)
            {
                return new KeyValuePair<MessageViewModel, AuthViewModel?>(
                new MessageViewModel { StatusCode = HttpStatusCode.Unauthorized, Message = "This user does not have membership" },
                null
                );
            }
            var viewModel = new KeyValuePair<MessageViewModel, AuthViewModel?>(
                new MessageViewModel { StatusCode = HttpStatusCode.OK, Message = "Authenticated" },
                _mapper.Map<AuthViewModel>(user)
                );
            var jwtToken = TokenManager.GenerateJwtToken(user.Email, user.DisplayName, user.UserType, user.Id, _configuration);
            viewModel.Value.JwtToken = jwtToken;
            return viewModel;
        }

        public bool Register(RegisterRequestModel requestModel)
        {
            // Check condition
            if (requestModel.Email.IsNullOrEmpty() || requestModel.Password.Length < 8)
            {
                return false;
            }

            var model = _userRepository.Create(requestModel);
            if (model == null) return false;
            return true;
        }

        public UserViewModel ShowUser()
        {
            var user = _userRepository.ShowUser();
            var guest = _userRepository.GetListGuest();
            var response = new UserViewModel
            {
                Total = user.Count,
                Guest = guest.Count,
                Member = user.Count - guest.Count
            };
            return response;
        }
    }
}
