using System;

namespace Core.Domain.Entities
{
	public class User
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string AccessKey { get; set; }
		public string Email { get; set; }
		public DateTime? CreatedIn { get; set; }
		public DateTime? LastUpdate { get; set; }
	}
}
