using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;

namespace DMSMVC.Service.Implementation
{
    public class StaffService : IStaffService
    {

        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IDepartmentRepository _departmentRepository;



        public StaffService(IStaffRepository staffRepository, IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IFileRepository fileRepository, IUserRepository userRepository)
        {
            _staffRepository = staffRepository;
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;

        }

        public async Task<bool> DeleteStaff(string email)
        {
            var staff = await _staffRepository.GetAsync(a => a.User.Email == email);
            var user = await _userRepository.GetAsync(a => a.Id == staff.UserId);
            _staffRepository.Delete(staff);
            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<StaffDto?> GetStaffById(Guid id)
        {
            var staff = await _staffRepository.GetAsync(p => p.Id == id);
            return staff != null ? new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Gender = (GenderEnum)staff.Gender!,
                Email = staff.User.Email,
                DepartmentName = staff.Department.DepartmentName,
                StaffNumber = staff.StaffNumber,
                Level = staff.Level,
                Role = staff.Role,
                ImageUrl = staff.ProfilePhotoUrl,
                phonenumber = staff.PhoneNumber
            } : null;
        }

        public async Task<StaffDto?> UpdateStaffAsync(Guid id, StaffDetailsModel staffDetailsModel)
        {
            var staff = await _staffRepository.GetAsync(a => a.Id == id);
            var department = await _departmentRepository.GetAsync(a => a.Id == staffDetailsModel.DepartmentId);
            if (staff == null) return null;
            staff.StaffNumber = staffDetailsModel.StaffNumber ?? staff.StaffNumber;
            staff.DepartmentId = department.Id;
            staff.Department = department;
            staff.Level = staffDetailsModel.Level ?? staff.Level;
            staff.FirstName = staffDetailsModel.FirstName ?? staff.FirstName;
            staff.LastName = staffDetailsModel.LastName ?? staff.LastName;
            staff.User.Email = staffDetailsModel.Email ?? staff.User.Email;
            staff.Gender = staffDetailsModel.Gender;
            staff.ProfilePhotoUrl = _fileRepository.Upload(staffDetailsModel.ProfilePhotoUrl);
            var staffToBeUpdated = _staffRepository.Update(staff);
            await _unitOfWork.SaveAsync();
            return new StaffDto
            {
                Id = staff.Id,
                FirstName = staff.FirstName,
                LastName = staff.LastName,
                Gender = (GenderEnum)staff.Gender!,
                Email = staff.User.Email,
                DepartmentName = staff.Department.DepartmentName,
                StaffNumber = staff.StaffNumber,
                Level = staff.Level,
                Role = staff.Role,
                ImageUrl = staff.ProfilePhotoUrl,
                phonenumber = staff.PhoneNumber
            };

        }

        //Id is the user id as the user object is created before user login and update the user
        public async Task<BaseResponse<StaffDto>> CreateAsync(Guid id, StaffDetailsModel staffDetailsModel)
        {
            var user = await _userRepository.GetAsync(a => a.Id == id);
            var staffExist = await _staffRepository.GetAsync(a => a.StaffNumber == staffDetailsModel.StaffNumber);
            if (staffExist != null)
            {
                var staff = await UpdateStaffAsync(id, staffDetailsModel);
                return new BaseResponse<StaffDto>
                {
                    Message = "Registration Successfull!!!",
                    Status = true,
                    Data = staff
                };
            }
            var department = await _departmentRepository.GetAsync(a => a.Id == staffDetailsModel.DepartmentId)!;
            var staffAdded = new Staff
            {
                UserId = id,
                Level = staffDetailsModel.Level,
                StaffNumber = staffDetailsModel.StaffNumber!,
                FirstName = staffDetailsModel.FirstName,
                LastName = staffDetailsModel.LastName,
                Gender = staffDetailsModel.Gender,
                PhoneNumber = staffDetailsModel.PhoneNumber,
                ProfilePhotoUrl = _fileRepository.Upload(staffDetailsModel.ProfilePhotoUrl),
                User = user,
                Department = department,
                DepartmentId = department.Id,
                Role = "Staff",
            };
            await _staffRepository.CreateAsync(staffAdded);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<StaffDto>
            {
                Message = "Registration Successfull!!!",
                Status = true,
                Data = new StaffDto
                {
                    Id = staffAdded.Id,
                    FirstName = staffAdded.FirstName,
                    LastName = staffAdded.LastName,
                    Email = user.Email,
                    Gender = staffDetailsModel.Gender,
                    ImageUrl = staffAdded.ProfilePhotoUrl,
                    StaffNumber = staffAdded.StaffNumber,
                    DepartmentName = staffAdded.Department.DepartmentName,
                    Level = staffAdded.Level,
                    Role = staffAdded.Role,
                }
            };

        }

        public async Task<BaseResponse<StaffDto>> GetStaffByStaffNumber(string staffNumberOrEmail)
        {
            var staff = await _staffRepository.GetAsync(a => (a.StaffNumber == staffNumberOrEmail) || (a.User.Email == staffNumberOrEmail));
            if (staff == null)
            {
                return new BaseResponse<StaffDto>
                {
                    Message = "User does not exist",
                    Status = false,
                    Data = null
                };
            }

            return new BaseResponse<StaffDto>
            {
                Status = true,
                Message = "Sucessfull",
                Data = new StaffDto
                {
                    Id = staff.Id,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Gender = (GenderEnum)staff.Gender!,
                    Email = staff.User.Email,
                    DepartmentName = staff.Department.DepartmentName,
                    StaffNumber = staff.StaffNumber,
                    Level = staff.Level,
                    Role = staff.Role,
                    ImageUrl = staff.ProfilePhotoUrl
                }
            };
        }

        public async Task<ICollection<StaffDto>?> GetStaffs(Guid departmentID)
        {
            var staffs = await _staffRepository.GetAllAsync();
            return staffs != null ? staffs.Where(a => a.DepartmentId == departmentID).Select(p =>
            new StaffDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Gender = (GenderEnum)p.Gender!,
                Email = p.User.Email,
                DepartmentName = p.Department.DepartmentName,
                StaffNumber = p.StaffNumber,
                Level = p.Level,
                Role = p.Role,
                ImageUrl = p.ProfilePhotoUrl,
                phonenumber = p.PhoneNumber
            }).ToList() : null;
        }

    }
}