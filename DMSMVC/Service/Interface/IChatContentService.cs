
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
    public interface IChatContentService
    {
        Task<BaseResponse<ICollection<ChatContentDTO>>> GetChatContentsAsync(ChatContentRequest request);
        Task<BaseResponse<ChatContentDTO>> CreateChatContentAsync(ChatContentRequest request);
        //void DeleteChatContent(Chat chat);


    }
}
