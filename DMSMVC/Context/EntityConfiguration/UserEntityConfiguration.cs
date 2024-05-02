using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Staff).WithOne(x => x.User);
            builder.HasData(
                new User
                {
                    Id = new Guid("0ebe9b6e-65a1-4b83-bf59-7c3a909519bd"),
                    Email = "admin@gmail.com",
                    Pin = "123456",
                });
        }
    }
}
