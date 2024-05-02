using DMSMVC.Models.DTOs;
using DMSMVC.Models.RequestModel;

namespace DMSMVC.Service.Interface
{
    public interface IStaffService
    {
        //bool Register(string FirstName, string LastName, string PhoneNumber, GenderEnum gender, string staffNumber, int departmentId, string level, string position, string email, string password, string SecurityQuestion, string SecurityAnswer);
        public Task<BaseResponse<StaffDto>> CreateAsync(Guid id, StaffDetailsModel staffDetailsModel);
        Task<bool> DeleteStaff(string email);
		Task<StaffDto> UpdateStaffAsync(Guid id, StaffDetailsModel staffDetailsModel);
        Task<StaffDto> GetStaffById(Guid id);
        Task<ICollection<StaffDto>> GetStaffs(Guid departmentId);
        Task<BaseResponse<StaffDto>> GetStaffByStaffNumber(string staffNumber);



    }
}
