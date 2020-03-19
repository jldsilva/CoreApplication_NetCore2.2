using System.Security.Cryptography;
using System.Text;

namespace Core.Application.Helpers
{
	public static class CryptoHelper
	{
		public static string GenerateSHA256String(string inputString)
		{
			var sha256 = SHA256.Create();
			var bytes = Encoding.UTF8.GetBytes(inputString);
			var hash = sha256.ComputeHash(bytes);

			return GetStringFromHash(hash);
		}

		public static string GenerateSHA512String(string inputString)
		{
			var sha512 = SHA512.Create();
			var bytes = Encoding.UTF8.GetBytes(inputString);
			var hash = sha512.ComputeHash(bytes);

			return GetStringFromHash(hash);
		}

		private static string GetStringFromHash(byte[] hash)
		{
			var result = new StringBuilder();

			for (int i = 0; i < hash.Length; i++)
			{
				result.Append(hash[i].ToString("X2"));
			}

			return result.ToString();
		}
	}
}
