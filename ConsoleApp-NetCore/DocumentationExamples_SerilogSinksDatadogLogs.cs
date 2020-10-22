using Serilog;
using System;

namespace ConsoleApp_NetCore
{
    public class DocumentationExamples_SerilogSinksDatadogLogs
    {
        /// <summary>
        /// https://github.com/DataDog/serilog-sinks-datadog-logs#example
        /// </summary>
        public static void Example_Serilog_Sink_Send_Events_and_logs_staight_away_to_Datadog(string apiKey)
        {
            //tested by Anton Vlasiuk
            // zero logs no the DataDog
            // additional check with Fiddler application (tool to track HTTP requests )
            // used Fiddler - zero requests were sent.
            var log = new LoggerConfiguration()
                .WriteTo.DatadogLogs(apiKey)
                .CreateLogger();

            var position = new { Latitude = 25, Longitude = 134 };
            var elapsedMs = 34;

            string message = $"Processed {position} in {elapsedMs:000} ms.";

            log.Information(message);

            Console.WriteLine(message);
        }
    }
}