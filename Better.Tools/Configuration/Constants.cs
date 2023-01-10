using Microsoft.Extensions.Logging;

namespace Better.Tools.Configuration
{
    public static class Constants
    {
        // Persistence - General
        public const string DbName = "Better";
        public const bool EnableSensitiveLogging = false;
        public const string ConnectionStringMain = $"Server={LocalHostServer};Database={DbName};Trusted_Connection={TrustedConnection};";
        public const string ConnectionStringNamed = "name=ConnectionStrings:DefaultConnection";
        public const string ConnectionStringAzure = $"Server={AzureServer};Initial Catalog={DbName}_db;Persist Security Info=False;User ID={UserId};Password={Secret};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout={ConnectionTimeout};";

        // Persistence - Local
        public const string LocalHostServer = "localhost";
        public const string TrustedConnection = "True";

        // Persistence - Azure
        public const string AzureServer = "tcp:server.database.windows.net,1433";
        public const string UserId = "**********";
        public const string Secret = "**********";
        public const string ConnectionTimeout = "30";

        // HttpFactory
        public const string BaseUrl = "https://localhost:7181/";
        public const string HeaderKey = "Authorization";
        public const string HeaderToken = "Bearer token";

        // Logs
        public const bool UseLogger = false;
        public const LogLevel DbLogLevel = LogLevel.Warning;
        public const ConsoleColor WarningLogColor = ConsoleColor.DarkGreen;
        public const ConsoleColor CriticalLogColor = ConsoleColor.DarkYellow;
        public const ConsoleColor ErrorLogColor = ConsoleColor.DarkRed;

        // Identity
        public const bool RequireEmailConfirmation = false;
        public const int LogCacheOut = 10;

        // Rules
        public const float MatchBetAmount = 4.00F;
        public const int MaxRepeatResult = 8;
        public const bool ShowResults = true;
        public const string ConnectionString = ConnectionStringAzure;
        public const int NumberOfMatches = 48;

        // Version
        public const string Version = "v1.7.6";
    }
}
