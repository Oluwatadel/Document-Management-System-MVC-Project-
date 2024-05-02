using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
	public class DocumentEntityConfiguration : IEntityTypeConfiguration<Document>
	{

		public void Configure(EntityTypeBuilder<Document> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.Staff).WithMany(x => x.Documents).HasForeignKey(x => x.StaffId);
			builder.HasOne(x => x.Department).WithMany(x => x.Documents).HasForeignKey(x => x.DepartmentId);
		}
	}
}
