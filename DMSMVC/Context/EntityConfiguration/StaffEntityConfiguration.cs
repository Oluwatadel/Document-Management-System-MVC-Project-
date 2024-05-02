using DMSMVC.Models.Entities;
using DMSMVC.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
    public class StaffEntityConfiguration : IEntityTypeConfiguration<Staff>
    {
        //private readonly IDepartmentRepository _departmentRepository;
        //private readonly IUnitOfWork _unitOfWork;

        //public StaffEntityConfiguration(IStaffRepository departmentRepository, IUnitOfWork unitOfWork)
        //{
        //    _departmentRepository = departmentRepository;
        //    _unitOfWork = unitOfWork;
        //}

        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Department).WithMany(x => x.Staffs).HasForeignKey(x => x.DepartmentId);
            builder.HasMany(x => x.Documents).WithOne(x => x.Staff);
            builder.HasOne(x => x.User).WithOne(x => x.Staff);
            builder.HasData(
                new Staff
                {
                    Id = new Guid("8072196e-21ec-48c5-b44f-2c27926abf8b"),
                    UserId = new Guid("0ebe9b6e-65a1-4b83-bf59-7c3a909519bd"),
                    StaffNumber = "49419",
                    DepartmentId = new Guid("6292ea0a-5efa-473d-a173-13227f259d80"),
                    Level = "12",
                    Role = "Admin",
                    FirstName = "admin",
                    LastName = "admin",
                    Gender = GenderEnum.Unknown,
                    PhoneNumber = "09038327372",
                    ProfilePhotoUrl = "tobi.jpg"
                    

                });
        }
    }
}
