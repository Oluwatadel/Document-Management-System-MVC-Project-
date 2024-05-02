using DMSMVC.Models.Entities;

namespace DMSMVC.Repository.Interface
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        public Department UpdateDepartment(Department type);

    }
}
