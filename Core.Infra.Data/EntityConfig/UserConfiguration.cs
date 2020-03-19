using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Data.EntityConfig
{
	public class UserConfiguration
	{
		public static void Configure(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>(model =>
			{
				model.ToTable("User");
				model.HasKey(x => x.UserId);
				model.Property(x => x.UserName).HasColumnName("UserName").HasMaxLength(255);
				model.Property(x => x.AccessKey).HasColumnName("AccessKey").HasMaxLength(255);
				model.Property(x => x.Email).HasColumnName("Email").HasMaxLength(50);
				model.Property(x => x.CreatedIn).HasColumnName("CreatedIn").HasDefaultValue(DateTime.Now);
				model.Property(x => x.LastUpdate).HasColumnName("LastUpdate");
			});
		}
	}
}
