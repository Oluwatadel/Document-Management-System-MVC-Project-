using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DMSMVC.Context.EntityConfiguration
{
	public class ChatContentEntityConfiguration : IEntityTypeConfiguration<ChatContent>
	{
		public void Configure(EntityTypeBuilder<ChatContent> builder)
		{
			builder.HasKey(x => x.Id);
			builder.HasOne(x => x.Chat).WithMany(x => x.ChatContents).HasForeignKey(x => x.ChatId);
		}
	}
}
