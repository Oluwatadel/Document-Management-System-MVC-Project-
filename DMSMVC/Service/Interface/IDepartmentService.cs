using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;

namespace DMSMVC.Service.Interface
{
    public interface IDepartmentService
    {
        Task<BaseResponse<DepartmentDTO>> CreateDepartmentAsync(DepartmentRequestModel request);
        Task<BaseResponse<DepartmentDTO>> GetDepartmentAsync(string exp);
        Task<BaseResponse<DepartmentDTO>> GetDepartmentByIdAsync(Guid id);
        Task<BaseResponse<ICollection<DepartmentDTO>>> GetAllDepartment();
		Task<BaseResponse<DepartmentDTO>> DeleteDepartment(Guid id);
        Task<BaseResponse<DepartmentDTO>> MakeAStaffHeadOfDepartment(string departmentName, string staffNumber);
		Task<BaseResponse<DepartmentDTO>> UpdateDepartment(Guid id, DepartmentUpdateModel request);
        Task<ICollection<Staff>> GetAllStaffOfADepartment(Guid id   );
        //String DepartmentChoice(DepartmentEnum department);
    }
}
