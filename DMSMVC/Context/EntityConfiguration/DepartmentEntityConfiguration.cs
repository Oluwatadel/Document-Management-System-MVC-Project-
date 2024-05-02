using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
    public class DepartmentEntityConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Staffs).WithOne(x => x.Department);
            builder.HasMany(x => x.Documents).WithOne(x => x.Department);
            builder.HasData(
                new Department
                {
                    Id = new Guid("6292ea0a-5efa-473d-a173-13227f259d80"),
                    DepartmentName = "Backend",
                    Acronym = "BCK",
                }
                );
        }
    }
}
