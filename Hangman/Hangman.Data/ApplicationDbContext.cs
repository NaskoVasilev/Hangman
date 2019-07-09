using Hangman.Models;
using Microsoft.EntityFrameworkCore;

namespace Hangman.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<ApplicationUser> Users { get; set; }

		public DbSet<ApplicationRole> Roles { get; set; }

		public DbSet<ApplicationUserRole> UserRoles { get; set; }

		public DbSet<Word> Words { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ApplicationUserRole>()
				.HasKey(ur => new { ur.UserId, ur.RoleId });

			base.OnModelCreating(modelBuilder);
		}
	}
}
