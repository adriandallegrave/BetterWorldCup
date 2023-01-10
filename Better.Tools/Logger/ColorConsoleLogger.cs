using Microsoft.Extensions.Logging;

namespace Better.Tools.Logger
{
    public sealed class ColorConsoleLogger : ILogger
    {
        private readonly Func<ColorConsoleLoggerConfiguration> _getCurrentConfig;
        private readonly string _name;

        public ColorConsoleLogger(string name, Func<ColorConsoleLoggerConfiguration> getCurrentConfig)
        {
            (_name, _getCurrentConfig) = (name, getCurrentConfig);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return default!;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _getCurrentConfig().LogLevelToColorMap.ContainsKey(logLevel);
        }

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var config = _getCurrentConfig();
            if (config.EventId == 0 || config.EventId == eventId.Id)
            {
                var originalColor = Console.ForegroundColor;
                var logLevelColor = config.LogLevelToColorMap[logLevel];

                var textHead = formatter(state, exception)[..formatter(state, exception).IndexOf("|")];
                var textTail = formatter(state, exception)[((formatter(state, exception).IndexOf("|")) + 1)..];

                Console.ForegroundColor = logLevelColor;
                Console.Write($"[{_name[(_name.LastIndexOf(".") + 1)..]}] ");

                Console.ForegroundColor = originalColor;
                Console.Write($"{textHead} ");

                Console.ForegroundColor = logLevelColor;
                Console.Write($"{textTail}");

                Console.ForegroundColor = originalColor;
                Console.WriteLine();
            }
        }
    }
}
