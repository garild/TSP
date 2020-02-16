using Serilog.Events;

namespace Tsp.Serilog.Options
{
    public class LoggerConfigurationOptions
    {
        public bool ClearProviders { get; set; }
        public string SectionName { get; set; } = "Serilog";
        public LogEventLevel MinimumLevelLog { get; set; } = LogEventLevel.Information;

        public LogEventLevel OverrideDefaultLevelLog { get; set; } = LogEventLevel.Warning;

        public bool WriteToFile { get; set; }

        public bool UseEnvironmentVariables { get; set; }

    }
}
