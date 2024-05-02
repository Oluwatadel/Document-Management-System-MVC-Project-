using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMSMVC.Service.Interface
{
    public interface IChatService
    {
        Task<BaseResponse<ICollection<ChatContentDTO>>> CreateOrContinueChat(ChatRequestModel request);
		Task DeleteChat(ChatRequestModel request);
        Task<BaseResponse<ChatDTO>> GetChat(ChatRequestModel request);
        Task<BaseResponse<ICollection<ChatDTO>>> GetAllChatForAStaff(string Email);
        //int ChatNotification(Staff staff);
    }
}
