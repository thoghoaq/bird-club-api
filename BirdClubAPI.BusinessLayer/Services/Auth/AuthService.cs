using AutoMapper;
using BirdClubAPI.DataAccessLayer.Repositories.User;
using BirdClubAPI.Domain.Commons.Utils;
using BirdClubAPI.Domain.DTOs.Request.Auth;
using BirdClubAPI.Domain.DTOs.View.Auth;
using BirdClubAPI.Domain.DTOs.View.Common;
using Microsoft.Extensions.Configuration;
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
            var viewModel = new KeyValuePair<MessageViewModel, AuthViewModel?>(
                new MessageViewModel { StatusCode = HttpStatusCode.OK, Message = "Authenticated" },
                _mapper.Map<AuthViewModel>(user)
                );
            var jwtToken = TokenManager.GenerateJwtToken(user.Email, user.DisplayName, user.UserType, user.Id, _configuration);
            viewModel.Value.JwtToken = jwtToken;
            return viewModel;
        }
    }
}
