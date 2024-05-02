using DMSMVC.Models.DTOs;
using DMSMVC.Models.RequestModel;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.Runtime.Serialization.Formatters.Binary;

namespace DMSMVC.Controllers
{
	//[Route("documents")]
    public class DocumentController : Controller
	{
		private readonly IDepartmentService _departmentService;
		private readonly IDocumentService _documentService;
		private readonly IStaffService _staffService;
		private readonly IUserService _userService;
		public readonly IIdentityService _identityService;
		private readonly IWebHostEnvironment _environment;

		public DocumentController(IDepartmentService departmentService, IDocumentService documentService, IStaffService staffService, IUserService userService, IIdentityService identityService, IWebHostEnvironment environment)
		{
			_departmentService = departmentService;
			_documentService = documentService;
			_staffService = staffService;
			_userService = userService;
			_identityService = identityService;
			_environment = environment;
		}

		[Authorize(Roles = "Admin, Staff")]
		public async Task<IActionResult> ListAllDocument()
		{
            var currentUser = await _identityService.GetCurrentUser();
            ViewBag.Id = currentUser!.Id;
			var departmentOfLoggedInStaff = await _departmentService.GetDepartmentByIdAsync(currentUser.Staff.DepartmentId);
			var documentsOfTheDepartment = departmentOfLoggedInStaff.Data!.Documents;
			ICollection<DocumentDTO> viewDocument = documentsOfTheDepartment.Where(x => x.IsDeleted == false).Select(p => new DocumentDTO
            {
                Author = p.Author,
                TimeUploaded = p.TimeUploaded,
                DepartmentName = p.Department.DepartmentName,
                Description = p.Description,
                DocumentUrl = p.DocumentUrl,
                DocumentId = p.Id,
                Title = p.Title,
            }).ToList();
			TempData["UserImage"] = currentUser.Staff.ProfilePhotoUrl;
			TempData["Role"] = currentUser.Staff.Role;
			return View(viewDocument);
		}

		
		[Authorize(Roles = "Admin, Staff")]
		public async Task<IActionResult> UploadDocument(DocumentRequestModel request)
		{
			return View();
		}

		[Authorize(Roles = "Admin, Staff")]
		[HttpPost, ActionName("UploadDocument")]
        public async Task<IActionResult> UploadDocumentW(DocumentRequestModel request)
		{
            var currentUser = await _identityService.GetCurrentUser();
            ViewBag.Id = currentUser!.Id;
			request.Author = $"{currentUser.Staff!.LastName} {currentUser.Staff.FirstName}";
			var response = await _documentService.CreateAsync(currentUser.Staff!, request);
            TempData["Message"] = response.Message;
            return RedirectToAction("ListAllDocument");
        }

		[Authorize(Roles = "Admin, Staff")]
		public async Task<IActionResult> DeleteDocument(Guid id)
		{
			var deletedDocument = await _documentService.DeleteDocument(id);
			if(deletedDocument == false) return View("ListAllDocument");
			TempData["Message"] = "Delete Successfull!!!";
			return RedirectToAction("ListAllDocument");

		}
			}
}
