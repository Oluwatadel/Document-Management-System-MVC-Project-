using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
	public class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
	{
		public void Configure(EntityTypeBuilder<Chat> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasMany(x => x.ChatContents).WithOne(x => x.Chat).HasForeignKey(x => x.ChatId);
			
		}
	}
}
