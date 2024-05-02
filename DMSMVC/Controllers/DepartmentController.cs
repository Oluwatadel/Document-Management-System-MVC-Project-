using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Models.ViewModels;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DMSMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class DepartmentController : Controller
    {
        public readonly IUserService _userService;
        public readonly IStaffService _staffService;
        public readonly IDepartmentService _departmentService;
		public readonly IIdentityService _identityService;

		public DepartmentController(IUserService userService, IStaffService staffService, IDepartmentService departmentService, IIdentityService identityService)
		{
			_userService = userService;
			_staffService = staffService;
			_departmentService = departmentService;
			_identityService = identityService;
		}

		public async Task<IActionResult> ManageDepartment()
        {
            var departments =await  _departmentService.GetAllDepartment();
            ICollection<DepartmentViewModel> departmentViews = new List<DepartmentViewModel>();
            foreach(var department in departments.Data!)
            {
                var departmentView = (new DepartmentViewModel
                {
                    Acronym = department.Acronym,
                    DepartmentName = department.DepartmentName!,
                    Id = department.Id,
                    Documents = department.Documents,
                    Staffs = department.Staffs,
                });

                //Get the full name of the Director
                if (department.HeadOfDepartmentStaffNumber != null)
                {
					var hod = await _staffService.GetStaffByStaffNumber(department.HeadOfDepartmentStaffNumber);
                    departmentView.NameOfHod = $"{hod.Data?.LastName} {hod.Data!.FirstName}";
                }
				departmentViews.Add(departmentView);
			}
            return View(departmentViews);
        }

        public IActionResult CreateDepartment() 
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentRequestModel requestModel) 
        {
            var response = await _departmentService.CreateDepartmentAsync(requestModel);
            if (response.Message == "Department already exist")return View(requestModel);
			TempData["Message"] = response.Message;
            return RedirectToAction("ManageDepartment");

        }

        public async Task<IActionResult> Delete(Guid id) 
        {
            var response = await _departmentService.GetDepartmentByIdAsync(id);
            if (response.Data == null) return BadRequest();
            //TempData["Message"] = response.Message;
            return  View(response.Data);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var response = await _departmentService.DeleteDepartment(id);
            TempData["Message"] = response.Message;
            return RedirectToAction("ManageDepartment");
                
        }

        public async Task<IActionResult> UpdateDepartment(Guid id)
        {
            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department.Data == null) return RedirectToAction("ManageDepartment");
            return View(department.Data);
            
        }

        [HttpPost, ActionName("UpdateDepartment")]
        public async Task<IActionResult> Update(Guid id, DepartmentUpdateModel updateModel)
        {
            var department = await _departmentService.UpdateDepartment(id, updateModel);
			return RedirectToAction("ManageDepartment");

        }

        public async Task<IActionResult> DisplayAllStaffsOfDepartment(Guid id)
        {
            var currentUser = await _identityService.GetCurrentUser();
            ViewBag.Role = currentUser.Staff!.Role;
            var staff = await _staffService.GetStaffById(id);

            var department = await _departmentService.GetDepartmentByIdAsync(id);
            if (department.Data == null)
            {
                return View();
            }
            var staffs = new BaseResponse<ICollection<StaffDto>>
            {
                Status = true,
                Message = "Successfull",
                Data = department.Data.Staffs.Select(p => new StaffDto
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Id = p.Id,
                    DepartmentName = p.Department.DepartmentName,
                    StaffNumber = p.StaffNumber,
                    Level = p.Level,
                    Email = p.User.Email,
                    Gender = (GenderEnum)p.Gender!,
                    ImageUrl = p.ProfilePhotoUrl

                }).ToList()
            };
            return View(staffs.Data);
        }
    }
}
