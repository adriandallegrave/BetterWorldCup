using Better.Tools.Configuration;
using Microsoft.Extensions.Logging;

namespace Better.Tools.Logger
{
    public class ColorConsoleLoggerConfiguration
    {
        public int EventId { get; set; }

        public Dictionary<LogLevel, ConsoleColor> LogLevelToColorMap { get; set; } = new()
        {
            [LogLevel.Warning] = Constants.WarningLogColor,
            [LogLevel.Critical] = Constants.CriticalLogColor,
            [LogLevel.Error] = Constants.ErrorLogColor,
        };
    }
}
