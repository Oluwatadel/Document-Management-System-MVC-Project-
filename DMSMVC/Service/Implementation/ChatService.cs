using DMSMVC.Context;
using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;

namespace DMSMVC.Service.Implementation
{
    public class ChatService : IChatService
    {
        public readonly IChatRepository _chatRepository;
        public readonly IChatContentRepository _chatContentRepository;
        IUnitOfWork _unitOfWork;

        public ChatService(IChatRepository chatRepository, IUnitOfWork unitOfWork, IChatContentRepository chatContentRepository)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
            _chatContentRepository = chatContentRepository;

        }

        //To return List of ChatContents
        public async Task<BaseResponse<ICollection<ChatContentDTO>>> CreateOrContinueChat(ChatRequestModel request)
        {
            var chat = await _chatRepository.GetAsync(a => a.SenderEmail == request.SenderEmail || a.ReceiverEmail == request.SenderEmail);
            if (chat != null)
            {
                var chatChatContent = await _chatContentRepository.GetChatContents(chat.Id);
                return new BaseResponse<ICollection<ChatContentDTO>>
                {
                    Status = true,
                    Message = "Successfull",
                    Data = chatChatContent.Select(a => new ChatContentDTO
                    {
                        ChatContentReference = a.ChatContentReference,
                        ContentOfChat = a.ContentOfChat,
                        CreatedAt = a.CreatedAt
                    }).ToList()
                };
            }
            var newChat = new Chat
            {
                ReceiverEmail = request.ReceiverEmail,
                SenderEmail = request.SenderEmail,
            };

            //Create new chat and new chat content
            await _chatRepository.CreateAsync(newChat);
            await _chatContentRepository.CreateAsync(new ChatContent
            {
                ChatId = newChat.Id,
                ContentOfChat = request.Message,
                Chat = newChat,
            });
            await _unitOfWork.SaveAsync();
            return new BaseResponse<ICollection<ChatContentDTO>>
            {
                Status = true,
                Message = "Successfull",
                Data = null
            };
        }

        public async Task DeleteChat(ChatRequestModel request)
        {
            var chat = await _chatRepository.GetAsync(a => a.ReceiverEmail == request.SenderEmail);
            var chatContents = await _chatContentRepository.GetAllAsync();
            var contentToDelete = chatContents.Where(a => a.ChatId == request.Id).ToList();
            foreach(var content in chatContents)
            {
                _chatContentRepository.Delete(content);
            }
            _chatRepository.Delete(chat);
            await _unitOfWork.SaveAsync();
        }

        public async Task<BaseResponse<ICollection<ChatDTO>>> GetAllChatForAStaff(string email)
        {
            var chats = await _chatRepository.GetAllAsync();
            var allChatbySameStaff = chats.Where(a => a.ReceiverEmail == email || a.SenderEmail == email);
            return new BaseResponse<ICollection<ChatDTO>>
            {
                Status = true,
                Message = "Successfull",
                Data = chats.Select(a => new ChatDTO
                {
                    SenderEmail = a.SenderEmail,
                    ReceiverEmail = a.ReceiverEmail,
                    ChatContents = a.ChatContents
                }).ToList()
            };
        }

        public async Task<BaseResponse<ChatDTO>> GetChat(ChatRequestModel request)
        {
            var chat = await _chatRepository.GetAsync(a => (a.ReceiverEmail == request.ReceiverEmail || a.SenderEmail == request.ReceiverEmail) && a.IsDeleted == false);
            if (chat == null)
            {
                return new BaseResponse<ChatDTO>
                {
                    Message = "Successfull",
                    Status = true,
                    Data = null
                };
            }
            return new BaseResponse<ChatDTO>
            {
                Message = "Successfull",
                Status = true,
                Data = new ChatDTO
                {
                    SenderEmail = chat.SenderEmail,
                    ReceiverEmail = chat.ReceiverEmail,
                    ChatContents = chat.ChatContents
                }
            };
        }
    }
}
