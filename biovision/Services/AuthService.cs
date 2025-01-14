namespace biovision.Services {
	public class AuthService {
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor) {
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}

		public bool Authenticate(string username, string password) {
			var adminUsername = _configuration.GetSection("AdminCredentials")["Username"];
			var adminPassword = _configuration.GetSection("AdminCredentials")["Password"];

			return username == adminUsername && password == adminPassword;
		}

		public bool IsAuthenticated() {
			return _httpContextAccessor.HttpContext.Session.GetString("Admin") == "true";
		}
	}
}
