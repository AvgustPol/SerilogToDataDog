using Serilog.Core;

namespace ConsoleApp_NetCore
{
    internal class Program
    {
        public const string custom_apiKey = "";

        public const string custom_source = "";
        public const string custom_host = " ";
        public const string custom_service = "";

        public static string[] custom_tags = new string[] { "ENV:DEVELOP" };

        private static void Main(string[] args)
        {
            var test1 = InitDataDogLoggerWithoutAppsettings();
            var test2 = InitDataDogLogger(configuration);
            var test3 = InitDataDogLoggerOverride();

            test1.Information("Successfully configured agentless logging 1");
            test2.Information("Successfully configured agentless logging 2");
            test3.Information("Successfully configured agentless logging 3");
        }

        private static Logger InitDataDogLoggerWithoutAppsettings()
        {
            string url = "datadoghq.eu";

            var config = new DatadogConfiguration(
                url: url,
                port: 443,
                useSSL: true,
                useTCP: true);

            var logger = new LoggerConfiguration()
                .WriteTo
                .DatadogLogs(
                    apiKey: custom_apiKey,
                    source: custom_source,
                    service: custom_service,
                    host: custom_host,
                    tags: custom_tags,
                    configuration: config
                )
                .CreateLogger();

            return logger;
        }

        private static Logger InitDataDogLoggerOverride()
        {
            var config = new DatadogConfiguration(url: "tcp-intake.logs.datadoghq.eu", port: 443, useSSL: true, useTCP: true);
            var log = new LoggerConfiguration()
                .WriteTo.DatadogLogs(
                    apiKey: custom_apiKey,
                    source: custom_source,
                    service: custom_service,
                    host: custom_host,
                    tags: custom_tags,
                    configuration: config
                )
                .CreateLogger();

            return log;
        }

        private static Logger InitFileLogger()
        {
            var logger = new LoggerConfiguration()
                .WriteTo
                .File(new JsonFormatter(renderMessage: true), HARDCODED_LOCATION)
                .CreateLogger();

            return logger;
        }

        private static Logger InitDataDogLogger(IConfiguration configuration)
        {
            var dataDogLogger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return dataDogLogger;
        }
    }
}