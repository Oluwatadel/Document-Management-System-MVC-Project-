using DMSMVC.Models.DTOs;
using DMSMVC.Models.RequestModel;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Web.Controllers
{

    public class UserController : Controller
    {
        public readonly IUserService _userService;
        public readonly IStaffService _staffService;
        public readonly IDepartmentService _departmentService;
        public readonly IIdentityService _identityService;
        public readonly IRegexCheck _regexcheck;

        public UserController(IUserService userService, IStaffService staffService, IDepartmentService departmentService, IIdentityService identityService, IRegexCheck regexcheck)
        {
            _userService = userService;
            _staffService = staffService;
            _departmentService = departmentService;
            _identityService = identityService;
            _regexcheck = regexcheck;
        }

        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            var checkUser = await _userService.GetUserAsyn(a => a.Email == loginRequest.Email);
            if(checkUser == null)
            {
				TempData["Message"] = "User does not exist";
				return View(loginRequest);
			}
            var isValidCredntial = await _identityService.IsCredentialsValid(loginRequest);
            if (!isValidCredntial)
            {
                TempData["Message"] = "Invalid Credential!!";
				return View(loginRequest);
            }
            var user = await _userService.GetUserAsyn(a => a.Email == loginRequest.Email);

            //Read on this
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            

            if (user.Staff == null)
            {
                TempData["UserEmail"] = user.Email;

                var claimId = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var princ = new ClaimsPrincipal(claimId);
                var prop = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, princ, prop);
                return RedirectToAction("CreateStaff", "Staff");
            }
            claims.Add(new Claim(ClaimTypes.Name, $"{user.Staff.LastName} {user.Staff.FirstName}"));
            claims.Add(new Claim(ClaimTypes.Role, user.Staff.Role));
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            var property = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, property);
            if ( user.Staff.Role == "Admin")
            {
                TempData["Pic"] = user.Staff.ProfilePhotoUrl;
                return RedirectToAction("Dashboard", new StaffDto
                {
                    Id = user.Staff.Id,
					DepartmentName = user.Staff.Department.DepartmentName,
					StaffNumber = user.Staff.StaffNumber,
					Email = user.Email,
					FirstName = user.Staff.FirstName,
					LastName = user.Staff.LastName,
					Gender = (GenderEnum)user.Staff.Gender!,
					phonenumber = user.Staff.PhoneNumber,
					ImageUrl = user.Staff.ProfilePhotoUrl,
					Level = user.Staff.Level,
					Role = user.Staff.Role
				});
            }
            TempData["Pic"] = user.Staff.ProfilePhotoUrl;
            return RedirectToAction("StaffDashboard", new StaffDto
            {
                Id = user.Staff.Id,
                DepartmentName = user.Staff.Department.DepartmentName,
                StaffNumber = user.Staff.StaffNumber,
                Email = user.Email,
                FirstName = user.Staff.FirstName,
                LastName = user.Staff.LastName,
                Gender = (GenderEnum)user.Staff.Gender!,
                phonenumber = user.Staff.PhoneNumber,
                ImageUrl = user.Staff.ProfilePhotoUrl,
                Level = user.Staff.Level,
                Role = user.Staff.Role
            });

        }

		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");

		}

		public IActionResult RegisterUser()
        {
			return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserUpdateRequest request)
        {
            if(_regexcheck.IsValidEmail(request.Email) == false)
            {
                TempData["Error"] = "Enter a valid email in \"abc@xyz.com\"";
                return View();
            }
            if (_regexcheck.IsValidPin(request.Pin) == false)
            {
                TempData["Error"] = "Your pin must be numeric with length between 6 and 8";
                return View();
            }

            var user = await _userService.CreateUser(request);
            if(user.Data == null)
			{
                TempData["Error"] = user.Message;
				return View(request);
            }
            return View("Login");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Dashboard(StaffDto userDTO)
        {
            return View(userDTO);
        }

        [Authorize(Roles = "Staff")]
        public IActionResult StaffDashboard(StaffDto staffDTO)
        {
            return View(staffDTO);
        }
        
        
    }
}
