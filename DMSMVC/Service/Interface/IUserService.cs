using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using System.Linq.Expressions;

namespace DMSMVC.Service.Interface
{
    public interface IUserService
    {
        //Task<BaseResponse<UserDTO>> LoginAsync(LoginRequest loginRequest);
		Task<string?> ReturnPassWord(string email);
		Task<string?> ReturnSecurityQuestionForgottenID(string email);
		Task<User?> GetUserAsyn(Expression<Func<User, bool>> exp);
		Task<UserDTO?> UpdateEmailPassword(Guid id, UserUpdateRequest userUpdateRequest);
        Task<BaseResponse<UserDTO?>> CreateUser(UserUpdateRequest request);
        Task DeleteUser(Guid id);



    }
}
