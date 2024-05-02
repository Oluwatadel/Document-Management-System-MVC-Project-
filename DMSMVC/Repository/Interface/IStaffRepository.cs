using DMSMVC.Models.Entities;

namespace DMSMVC.Repository.Interface
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Staff Update(Staff staff);
    }
}
