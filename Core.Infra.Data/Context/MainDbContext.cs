using Core.Data.EntityConfig;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Data.Context
{
	public class MainDbContext : BaseDbContext
	{
		#region [DbSet Properties]
		//public DbSet<Entity> Entity { get; set; }
		public DbSet<User> User { get; set; }
		#endregion
		
		public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseSqlServer(Config.GetConnectionString("DefaultConnection"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//EntityConfiguration.Configure(modelBuilder);
			UserConfiguration.Configure(modelBuilder);
		}
	}
}
