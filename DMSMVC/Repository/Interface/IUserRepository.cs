

using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;

namespace DMSMVC.Repository.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        User Update(User user);
        bool IsExist(string userEmail);
    }
}
