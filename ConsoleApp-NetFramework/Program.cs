﻿using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace ConsoleApp_NetFramework
{
    internal class Program
    {
        public const string HARDCODED_SERILOGDEBUG_LOCATION = @"C:\Repos\tmp\LogsTest\SerilogDebug.txt";
        public const string HARDCODED_LOGS_LOCATION = @"C:\Repos\tmp\LogsTest\log.json";

        private static void Main(string[] args)
        {
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

            //// This one is not working.
            //// Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'
            //Logger logger1 = CreateLogger1("URL", "<API_KEY>");
        }

        ///// <summary>
        ///// https://docs.datadoghq.com/logs/log_collection/csharp/?tab=serilog#agentless-logging
        /////
        /////
        ///// </summary>
        ///// <returns></returns>
        //private static object CreateLogger1(string urlParam, string apiKey)
        //{
        //    var log = new LoggerConfiguration(url: urlParam)
        //    .WriteTo.DatadogLogs(apiKey)
        //    .CreateLogger();

        //    return log;
        //}
    }
}