using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Core.Data.Context
{
	public class BaseDbContext : DbContext
	{
		protected IConfigurationRoot Config { get; set; }

		public BaseDbContext() : base() { }

		public BaseDbContext(DbContextOptions options) : base(options) { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			Config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
		}
	}
}
