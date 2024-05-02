using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;
using System.Linq.Expressions;

namespace DMSMVC.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<string?> ReturnSecurityQuestionForgottenID(string email)
        {
            var user = await _userRepository.GetAsync(a => a.Email == email);
            return user != null ? user.Staff!.SecurityQuestion! : string.Empty;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _userRepository.GetAsync(a => a.Id == id);
            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task<BaseResponse<UserDTO?>> CreateUser(UserUpdateRequest request)
        {
            var user = await _userRepository.GetAsync(a => a.Email == request.Email);
            if (user != null)
            {
                return new BaseResponse<UserDTO?>
                {
                    Status = false,
                    Message = "User already exist!!",
                    Data = null
                };
            }
            if(request.Pin != request.ConfirmPin)
            {
                return new BaseResponse<UserDTO?>
                {
                    Status = false,
                    Message = "Password and confirm password must match!!",
                    Data = null
                };
            }
            if(int.TryParse(request.Pin, out int b) == false)
            {
                return new BaseResponse<UserDTO?>
                {
                    Status = false,
                    Message = "Password and confirm password must match!!",
                    Data = null
                };
            }
            var newUser = new User
            {
                Email = request.Email,
                Pin = request.Pin
            };
            var userReturned = await _userRepository.CreateAsync(newUser);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<UserDTO?>
            {
                Status = true,
                Message = "Successfull",
                Data = new UserDTO
                {
                    Email = userReturned.Email,
                    Id = userReturned.Id,
                }
            };
        }

        public async Task<UserDTO?> UpdateEmailPassword(Guid id, UserUpdateRequest request)
        {
            var user = await _userRepository.GetAsync(a => a.Id == id);
            if (user == null) return null;
            user.Email = request.Email ?? user.Email;
            if (request.Pin != request.ConfirmPin) return null;
            user.Pin = request.Pin;
            await _unitOfWork.SaveAsync();
            return new UserDTO
            {
                Email = user.Email,
                Pin = user.Pin,
                Id = id,
            };
        }

        public async Task<string?> ReturnPassWord(string email)
        {
            var user = await _userRepository.GetAsync(a => a.Email == email);
            return user != null ? user.Pin : null;
        }

        public async Task<User?> GetUserAsyn(Expression<Func<User, bool>> exp)
        {
            var user = await _userRepository.GetAsync(exp);
            return user != null ? user : null;
        }
    }
}
