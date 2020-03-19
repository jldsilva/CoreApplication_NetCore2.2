namespace Core.Api.TokenProvider
{
	public class TokenConfiguration
	{
		public int Seconds { get; set; }
		public string Audience { get; set; }
		public string Issuer { get; set; }
	}
}
