using Serilog;
using Serilog.Core;
using Serilog.Sinks.Datadog.Logs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp_NetCore
{
    internal class Program
    {
        private static readonly string _apiKey = "";
        private static readonly string _source = "serilog-to-datadog-poc";
        private static readonly string _host = "serilog-to-datadog-poc";
        private static readonly string _service = "serilog-to-datadog-poc";
        public static string[] _tags = new string[] { "env:develop" };

        public const string HARDCODED_SERILOGDEBUG_LOCATION = @"C:\Repos\tmp\LogsTest\SerilogDebug.txt";
        public const string HARDCODED_LOGS_LOCATION = @"C:\Repos\tmp\LogsTest\log.json";

        private static void Main(string[] args)
        {
            var file = File.CreateText(HARDCODED_SERILOGDEBUG_LOCATION);

            Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            Serilog.Debugging.SelfLog.Enable(Console.Error);
            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            #region Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'

            ////Documentation reference:
            ////https://docs.datadoghq.com/logs/log_collection/csharp/?tab=serilog#agentless-logging

            //// This one is not working.
            //Logger logger1 = new LoggerConfiguration(url: "URL")
            //.WriteTo.DatadogLogs("<API_KEY>")
            //.CreateLogger();

            #endregion Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'

            #region Issue 2 - SelfLog is not working (File sink)

            //var file = File.CreateText(HARDCODED_SERILOGDEBUG_LOCATION);

            //Serilog.Debugging.SelfLog.Enable(TextWriter.Synchronized(file));
            //Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
            //Serilog.Debugging.SelfLog.Enable(Console.Error);
            //Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            //ILogger logger = new LoggerConfiguration()
            //    .WriteTo
            //    .File(new JsonFormatter(renderMessage: true), HARDCODED_LOGS_LOCATION)
            //    .Enrich.WithThreadId()
            //    .Enrich.WithMachineName()
            //    .Enrich.FromLogContext()
            //    .CreateLogger();

            //string message = $"Issue 2 [ConsoleApp-NetCore] POC info - {DateTime.Now.ToLongTimeString()}";

            //logger.Warning(message);
            //logger.Information(message);
            //logger.Error(message);
            //logger.Fatal(message);

            #endregion Issue 2 - SelfLog is not working (File sink)

            #region Issue 3 - sending to the datadog is not working

            List<Logger> loggers = new List<Logger>();

            List<int> allPorts = new List<int>() { 443, 1883, 10516 };
            List<string> allEndpoints = new List<string>()
            {
                "agent-intake.logs.datadoghq.eu",
                "agent-http-intake.logs.datadoghq.eu",
                "http-intake.logs.datadoghq.eu",
                "tcp-intake.logs.datadoghq.eu",
                "lambda-intake.logs.datadoghq.eu",
                "lambda-http-intake.logs.datadoghq.eu",
                "functions-intake.logs.datadoghq.eu"
            };
            List<bool> boolFlags = new List<bool>() { true, false };

            loggers.Add(CreateDataDogLogger());

            foreach (string endpoint in allEndpoints)
            {
                foreach (int port in allPorts)
                {
                    foreach (bool boolFlag1 in boolFlags)
                    {
                        foreach (bool boolFlag2 in boolFlags)
                        {
                            loggers.Add(CreateDataDogLoggerOverride(endpoint, port, boolFlag1, boolFlag2));
                        }
                    }
                }
            }

            foreach (var logger in loggers)
            {
                logger.Warning($"Agentless test succeeded.");
                logger.Information($"Agentless test succeeded.");
                logger.Error($"Agentless test succeeded.");
                logger.Fatal($"Agentless test succeeded.");
            }

            #endregion Issue 3 - sending to the datadog is not working
        }

        public static Logger CreateDataDogLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.DatadogLogs(_apiKey)
                .CreateLogger();
        }

        public static Logger CreateDataDogLoggerOverride(string urlParam, int portParam, bool useSSLParam, bool useTCPParam)
        {
            var config = new DatadogConfiguration(
                    url: urlParam,
                    port: portParam,
                    useSSL: useSSLParam,
                    useTCP: useTCPParam);

            var log = new LoggerConfiguration()
                .WriteTo.DatadogLogs(
                    apiKey: _apiKey,
                    source: _source,
                    service: _service,
                    host: _host,
                    tags: _tags,
                    configuration: config
                )
                .CreateLogger();

            return log;
        }
    }
}