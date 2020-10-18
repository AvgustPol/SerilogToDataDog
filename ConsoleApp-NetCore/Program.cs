using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp_NetCore
{
    internal class Program
    {
        public const string HARDCODED_SERILOGDEBUG_LOCATION = @"C:\Repos\tmp\LogsTest\SerilogDebug.txt";
        public const string HARDCODED_LOGS_LOCATION = @"C:\Repos\tmp\LogsTest\log.json";

        private static void Main(string[] args)
        {
            #region Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'

            ////Documentation reference:
            ////https://docs.datadoghq.com/logs/log_collection/csharp/?tab=serilog#agentless-logging

            //// This one is not working.
            //Logger logger1 = new LoggerConfiguration(url: "URL")
            //.WriteTo.DatadogLogs("<API_KEY>")
            //.CreateLogger();

            #endregion Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'

            #region Issues 2 - SelfLog is not working

            var file = File.CreateText(HARDCODED_SERILOGDEBUG_LOCATION);

            Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            ILogger logger = new LoggerConfiguration()
                .WriteTo
                .File(new JsonFormatter(renderMessage: true), HARDCODED_LOGS_LOCATION)
                .Enrich.WithThreadId()
                .Enrich.WithMachineName()
                .Enrich.FromLogContext()
                .CreateLogger();

            string message = $"Issue 2 [ConsoleApp-NetCore] POC info - {DateTime.Now.ToLongTimeString()}";

            logger.Warning(message);
            logger.Information(message);
            logger.Error(message);
            logger.Fatal(message);

            #endregion Issues 2 - SelfLog is not working
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
        //        .File(new JsonFormatter(renderMessage: true), HARDCODED_LOGS_LOCATION)
        //        .CreateLogger();

        //    return logger;
        //}
    }
}