using System;

namespace TeslaLib.TeslaModels
{
	public class AuthenticationResponse
	{
		public string AccessToken { get; set; }
		public string TokenType { get; set; }
		public long ExpiresIn { get; set; }
		public string RefreshToken { get; set; }
		public long CreatedAt { get; set; }

		public DateTime CreatedAtLocal => DateTimeOffset.FromUnixTimeSeconds(CreatedAt).LocalDateTime;
		public DateTime CreatedAtUtc => DateTimeOffset.FromUnixTimeSeconds(CreatedAt).UtcDateTime;
		public DateTime ExpiresLocal => DateTimeOffset.FromUnixTimeSeconds(CreatedAt + ExpiresIn).LocalDateTime;
		public DateTime ExpiresUtc => DateTimeOffset.FromUnixTimeSeconds(CreatedAt + ExpiresIn).UtcDateTime;
	}
}
