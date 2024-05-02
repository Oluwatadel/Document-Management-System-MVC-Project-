using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Implementation;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;

namespace DMSMVC.Service.Implementation
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileRepository _fileRepository;

        public DocumentService(IDocumentRepository documentRepository, IDepartmentRepository departmentRepository, IStaffRepository staffRepository, IUnitOfWork unitOfWork, IFileRepository fileRepository)
        {
            _documentRepository = documentRepository;
            _departmentRepository = departmentRepository;
            _staffRepository = staffRepository;
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
        }

        public async Task<BaseResponse<DocumentDTO>> CreateAsync(Staff staff, DocumentRequestModel request)
        {
            //You are to use claims here
            var document = await _documentRepository.GetAsync(a => a.Title == request.Title);
            if(document != null)
            {
				var documentToUpload = new Document
				{
					Title = $"{request.Title}copy by {staff.FirstName}",
					Description = request.Description!,
                    StaffId = staff.Id,
					DepartmentId = staff.DepartmentId,
					DocumentUrl = _fileRepository.Upload(request.File),
					Author = staff.LastName + " " + staff.LastName,
					IsDeleted = request.IsDeleted
				};
				await _documentRepository.CreateAsync(documentToUpload);
				await _unitOfWork.SaveAsync();
				return new BaseResponse<DocumentDTO>
				{
					Status = true,
					Message = "Upload successfull",
					Data = new DocumentDTO
					{
                        DepartmentName = documentToUpload.Department.DepartmentName,
                        DocumentId = document.Id,
						Title = documentToUpload.Title,
						Description = documentToUpload.Description,
						Author = documentToUpload.Author,
						TimeUploaded = documentToUpload.TimeUploaded,
						DocumentUrl = documentToUpload.DocumentUrl,
					}
				};
			}
            var documentUploaded = new Document
            {
                Title = request.Title!,
                Description = request.Description!,
                StaffId = staff.Id,
                DepartmentId = staff.DepartmentId,
                DocumentUrl = _fileRepository.Upload(request.File),
                Author = staff.LastName + " " + staff.FirstName,
                IsDeleted = request.IsDeleted
            };

            await _documentRepository.CreateAsync(documentUploaded);
			await _unitOfWork.SaveAsync();
                
            return new BaseResponse<DocumentDTO>
            {
                Status = true,
                Message = "Upload successfull",
                Data = new DocumentDTO
                {
                    Title = documentUploaded.Title,
                    Description = documentUploaded.Title,
                    Author = documentUploaded.Author,
                    TimeUploaded = documentUploaded.TimeUploaded,
                    DocumentUrl = documentUploaded.DocumentUrl,
                }
            };
        }

        public async Task<bool> DeleteDocument(DocumentRequestModel request)
        {
            var document = await _documentRepository.GetAsync(a => a.Title == request.Title);
            document.IsDeleted = true;
            await _unitOfWork.SaveAsync();
            return true;
        }

		public async Task<bool> DeleteDocument(Guid id)
		{
            var document = await _documentRepository.GetAsync(a => a.Id == id);
            if (document == null) return true;
            document.IsDeleted = true;
			await _unitOfWork.SaveAsync();

			return true;
		}

		public async Task<BaseResponse<ICollection<DocumentDTO>>> DisplayAllDocuments(Guid departmentId)
        {
            var documentsToDisplay = await _documentRepository.GetAllAsync();
            var displayDocuments = documentsToDisplay.Where(a => a.DepartmentId == departmentId && a.IsDeleted == false)
                .Select(a => new DocumentDTO
                {
                    DocumentId = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Author = a.Author,
                    DepartmentName = a.Department.DepartmentName,
                    DocumentUrl = a.DocumentUrl,
                    TimeUploaded = a.TimeUploaded,
                }).ToList();
            return new BaseResponse<ICollection<DocumentDTO>>
            {
                Status = true,
                Message = "Successfull",
                Data = displayDocuments,
            };
        }

        public async Task<string> DownloadDocument(Guid id)
        {
            var document = await _documentRepository.GetAsync(a => a.Id == id);
			return document.DocumentUrl;
		}

        public async Task<BaseResponse<DocumentDTO>> SearchDocument(string title)
        {
            var searchedDocument =await _documentRepository.GetAsync(a => a.Title == title);
            return new BaseResponse<DocumentDTO>
            {
                Status = true,
                Message = $"Document found",
                Data = new DocumentDTO
                {
                    Title = searchedDocument.Title,
                    Description = searchedDocument.Description,
                    Author = searchedDocument.Author,
                    DepartmentName = searchedDocument.Department.DepartmentName,
                    DocumentUrl = searchedDocument.DocumentUrl,
                }
            };
        }

    }
}

        