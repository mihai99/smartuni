using Domain;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class EntityFrameworkContext : DbContext
	{
		public DbSet<Student> Students { get; set; }
		public DbSet<Group> Groups { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public EntityFrameworkContext(DbContextOptions options): base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Notes>().HasNoKey();
		}
	}
}
