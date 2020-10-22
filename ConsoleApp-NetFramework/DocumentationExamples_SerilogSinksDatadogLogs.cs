using Serilog;

namespace ConsoleApp_NetCore
{
    public class DocumentationExamples_SerilogSinksDatadogLogs
    {
        /// <summary>
        /// https://github.com/DataDog/serilog-sinks-datadog-logs#example
        /// </summary>
        public static void Example_Serilog_Sink_Send_Events_and_logs_staight_away_to_Datadog(string apiKey)
        {
            var log = new LoggerConfiguration()
                .WriteTo.DatadogLogs(apiKey)
                .CreateLogger();

            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            log.Information("Processed {@Position} in {Elapsed:000} ms.", position, elapsedMs);
        }
    }
}