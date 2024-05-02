using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;

namespace DMSMVC.Service.Implementation
{
    public class ChatContentService : IChatContentService
    {
        private readonly IChatContentRepository _chatContentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ChatContentService(IChatContentRepository chatContentRepository, IUnitOfWork unitOfWork)
        {
            _chatContentRepository = chatContentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<ChatContentDTO>> CreateChatContentAsync(ChatContentRequest request)
        {
            var newContent = new ChatContent
            {
                ChatId = request.Id,
                ContentOfChat = request.ContentOfChat,
            };
            await _chatContentRepository.CreateAsync(newContent);
            await _unitOfWork.SaveAsync();
            return new BaseResponse<ChatContentDTO>
            {
                Status = true,
                Message = "Sent",
                Data = new ChatContentDTO
                {
                    ChatContentReference = newContent.ChatContentReference,
                    ContentOfChat = newContent.ContentOfChat
                }
            };
        }

        public async Task<BaseResponse<ICollection<ChatContentDTO>>> GetChatContentsAsync(ChatContentRequest request)
        {
            var chatcontent = await _chatContentRepository.GetAllAsync();
           
            if (chatcontent == null)
            {
                return new BaseResponse<ICollection<ChatContentDTO>>
                {
                    Status = true,
                    Message = "No message",
                    Data = null
                };
            }
			var chatContents = chatcontent.Where(a => a.ChatId == request.Id)
			   .Select(p => new ChatContentDTO
			   {
				   ChatContentReference = p.ChatContentReference,
				   ContentOfChat = p.ContentOfChat,
			   });
			return new BaseResponse<ICollection<ChatContentDTO>>
            {
                Status = true,
                Message = "Message",
                Data = chatContents.ToList()
            };
        }

	}
}
