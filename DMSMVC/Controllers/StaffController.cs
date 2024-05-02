using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Service.Implementation;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace DMSMVC.Controllers
{
    //[Route("staff")]
    public class StaffController : Controller
    {
        public readonly IUserService _userService;
        public readonly IStaffService _staffService;
        private readonly IDepartmentService _departmentService;
        public readonly IRegexCheck _regexcheck;
        public readonly IIdentityService _identityService;

        public StaffController(IUserService userService, IStaffService staffService, IDepartmentService departmentService, IRegexCheck regexcheck, IIdentityService identityService)
        {
            _userService = userService;
            _staffService = staffService;
            _departmentService = departmentService;
            _regexcheck = regexcheck;
            _identityService = identityService;
        }

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SearchForAStaff()
        {
            return View();
        }

        [HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SearchForAStaff(string emailOrStaffNumber)
        {
            var staff = await _staffService.GetStaffByStaffNumber(emailOrStaffNumber);
            if (staff.Data == null)
            {
                TempData["Message"] = "Staff does not exist!!!";
                return View();
            }
            return RedirectToAction("StaffDetail", staff.Data);

            //return RedirectToAction("StaffDetail", staff.Data);
        }



        public async Task<IActionResult> StaffDetail(StaffDto staff)
        {
            var currentUser = await _staffService.GetStaffById(staff.Id);
            ViewBag.Role = currentUser.Role;
            return View(currentUser);
        }

		[Authorize(Roles = "Admin, Staff")]
		[HttpGet, ActionName("StaffDetail")]
        public async Task<IActionResult> StaffDetail(Guid id)
        {
            var currentUser = await _identityService.GetCurrentUser();
            ViewBag.Role = currentUser!.Staff!.Role;
            var staff = await _staffService.GetStaffById(id);
            //var path = staff.ImageUrl.Split("\\");
            return RedirectToAction("StaffDetails", staff);
        }

		[Authorize(Roles = "Admin, Staff")]
		public IActionResult StaffDetails(StaffDto staff)
        {
            return View(staff);
        }


        //[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateStaff()
        {

            var email = TempData["UserEmail"];
            if (email == null)
            {
                return Unauthorized();
            }
                ViewBag.Email = email;
            var departments = await _departmentService.GetAllDepartment();
            ViewBag.departments = new SelectList(departments.Data, "Id", "DepartmentName");
            return View();
        }

		//[Authorize(Roles = "Admin")]
		[HttpPost, ActionName("CreateStaff")]
        public async Task<IActionResult> CreateStaffObj(StaffDetailsModel staffDetailsModel)
        {
            var departments = await _departmentService.GetAllDepartment();
            ViewBag.departments = new SelectList(departments.Data, "Id", "DepartmentName");
            if (_regexcheck.IsValidEmail(staffDetailsModel.Email!) == false)
            {
                TempData["Error"] = "Enter a valid email in \"abc@xyz.com\"";
                return View();
            }
            if (_regexcheck.IsValidPhoneNumber(staffDetailsModel.PhoneNumber!) == false)
            {
                TempData["Error"] = "Enter a correct phone number";
                return View();
            }
            var currentUser = await _identityService.GetCurrentUser();
            TempData["UserEmail"] = currentUser!.Email;
            await _staffService.CreateAsync(currentUser!.Id, staffDetailsModel);
            TempData["UserEmail"] = currentUser.Email;
            return RedirectToAction("Login", "User");
        }


		[Authorize(Roles = "Admin, Staff")]
		public async Task<IActionResult> UpdateStaff([FromRoute] Guid id)
        {
            var staff = await _staffService.GetStaffById(id);
            if (staff == null)
            {
                return RedirectToAction("Dashboard", "User");
            }
            var departments = await _departmentService.GetAllDepartment();
            ViewBag.departments = new SelectList(departments.Data, "Id", "DepartmentName");
            return View(staff);
        }

        [HttpPost]
		[Authorize(Roles = "Admin, Staff")]
		public async Task<IActionResult> UpdateStaff(Guid id, [FromForm]StaffDetailsModel requestModel)
        {
            var staff = await _staffService.UpdateStaffAsync(id, requestModel);
            var department = await _departmentService.GetDepartmentAsync(staff.DepartmentName);
            var staffs = await _departmentService.GetAllStaffOfADepartment(department.Data!.Id);

            return RedirectToAction("StaffDashboard", "User", staff);
        }


        
    }
}
