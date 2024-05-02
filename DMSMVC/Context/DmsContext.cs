using DMSMVC.Context.EntityConfiguration;
using DMSMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DMSMVC.Context
{
    public class DmsContext : DbContext
    {
        public DmsContext(DbContextOptions<DmsContext> opt) : base(opt)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StaffEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ChatContentEntityConfiguration());
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ChatContent> ChatContents { get; set; }
        public DbSet<Chat> Chats { get; set; }

    }
}
