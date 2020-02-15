using Serilog.Events;

namespace Tsp.Serilog.Options
{
    public class LoggerConfigurationOptions
    {
        public bool ClearProviders { get; set; }
        public string SectionName { get; set; } = "Serilog";
        public LogEventLevel MinimumLevelLog { get; set; }

        public LogEventLevel OverrideDefaultLevelLog { get; set; }

        public bool WriteToFile { get; set; }

        public bool UseEvniromentVariables { get; set; }

    }
}
