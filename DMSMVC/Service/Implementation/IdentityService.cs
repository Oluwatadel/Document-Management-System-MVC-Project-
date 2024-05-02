using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;
using System.Security.Claims;

namespace DMSMVC.Service.Implementation
{
	public class IdentityService : IIdentityService
	{
		private readonly IUserRepository _userRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetDepartment(string email)
		{
			var user = await _userRepository.GetAsync(a => a.Email == email);
			return user.Staff.Department.DepartmentName;
		}

		public async Task<bool> IsCredentialsValid(LoginRequestModel loginRequest)
		{
			var user = await _userRepository.GetAsync(a => a.Email == loginRequest.Email);
			if(user != null)
			{
				if(user.Pin == loginRequest.Pin)
				{
					return true;
				}
				return false;
			}
			return false;
		}

		public async Task<User?> GetCurrentUser()
		{
			//Throwing null reference
			var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
			var user = await _userRepository.GetAsync(a => a.Id == Guid.Parse(userId));
			return user;
		}
	}
}
