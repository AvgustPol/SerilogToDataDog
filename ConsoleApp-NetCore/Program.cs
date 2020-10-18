using Serilog;
using Serilog.Core;

namespace ConsoleApp_NetCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // This one is not working.
            // Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'
            Logger logger1 = CreateLogger1("URL", "<API_KEY>");
        }

        /// <summary>
        /// Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'
        ///
        /// Documentation reference:
        /// https://docs.datadoghq.com/logs/log_collection/csharp/?tab=serilog#agentless-logging
        /// </summary>
        /// <returns></returns>
        private static Logger CreateLogger1(string urlParam, string apiKey)
        {
            var log = new LoggerConfiguration(url: urlParam)
            .WriteTo.DatadogLogs(apiKey)
            .CreateLogger();

            return log;
        }

        //private static void Main(string[] args)
        //{
        //    //not working
        //    //Logger logger1 = CreateLogger1("http-intake.logs.datadoghq.eu", custom_apiKey);

        //    Logger test1 = InitDataDogLoggerWithoutAppsettings();
        //    Logger test2 = InitDataDogLoggerOverride();

        //    test1.Information("Successfully configured agentless logging 1");
        //    test2.Information("Successfully configured agentless logging 1");
        //}

        ///// <summary>
        ///// https://docs.datadoghq.com/logs/log_collection/csharp/?tab=serilog#agentless-logging
        /////
        /////
        ///// </summary>
        ///// <returns></returns>
        //private static object CreateLogger1(string urlParam, string apiKey)
        //{
        //    Logger log = new LoggerConfiguration(url: urlParam)
        //        .WriteTo.DatadogLogs("<API_KEY>")
        //        .CreateLogger();

        //    return log;
        //}

        //private static Logger InitDataDogLoggerWithoutAppsettings()
        //{
        //    string url = "datadoghq.eu";

        //    var config = new DatadogConfiguration(
        //        url: url,
        //        port: 443,
        //        useSSL: true,
        //        useTCP: true);

        //    var logger = new LoggerConfiguration()
        //        .WriteTo
        //        .DatadogLogs(
        //            apiKey: custom_apiKey,
        //            source: custom_source,
        //            service: custom_service,
        //            host: custom_host,
        //            tags: custom_tags,
        //            configuration: config
        //        )
        //        .CreateLogger();

        //    return logger;
        //}

        //private static Logger InitDataDogLoggerOverride()
        //{
        //    var config = new DatadogConfiguration(url: "tcp-intake.logs.datadoghq.eu", port: 443, useSSL: true, useTCP: true);
        //    var log = new LoggerConfiguration()
        //        .WriteTo.DatadogLogs(
        //            apiKey: custom_apiKey,
        //            source: custom_source,
        //            service: custom_service,
        //            host: custom_host,
        //            tags: custom_tags,
        //            configuration: config
        //        )
        //        .CreateLogger();

        //    return log;
        //}

        //private static Logger InitFileLogger()
        //{
        //    var logger = new LoggerConfiguration()
        //        .WriteTo
        //        .File(new JsonFormatter(renderMessage: true), HARDCODED_LOCATION)
        //        .CreateLogger();

        //    return logger;
        //}
    }
}