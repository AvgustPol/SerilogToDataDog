# [ Main issue ] Agentless logging is not working 

This solution was created to demonstrate issue with serilog agentless sending logs to the DataDog 

⚠ WARNING 
Important: we are using agentless logging, which means we don't have any installed agents - we are using Serilog to send logs from our application to directly the 
Datadog, and avoiding installing agents on our machines

## Table of contents

⚠ ⚠ ⚠ ⚠ ⚠ 
Direct link to core problem : [Agentless logging is not working](#issue-3---serilog-c-code-not-the-agent-is-not-sending-logs-to-the-datadog)  

- Projects description 
  * [.Net CORE](#consoleapp-netcore-net-core-31)
  * [.Net Framework](#consoleapp-netframework-net-framework-45)
- [Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'](#issue-1---loggerconfiguration-doesnt-have-url-parameter-named-url)
- [Issue 2 - Selflog is not working](#issue-2---selflog-is-not-working)
- [Issue 3 - serilog C# code (NOT the agent.) is not sending logs to the datadog](#issue-3---serilog-c-code-not-the-agent-is-not-sending-logs-to-the-datadog)

# Projects description:  

## **ConsoleApp-NetCore**: .NET CORE 3.1


	[ Serilog ] Version="2.10.0"  
	[ Serilog.Enrichers.Environment ] Version="2.1.3"  
	[ Serilog.Enrichers.Thread] Version="3.1.0"
	[ Serilog.Enrichers.Thread] Version="3.1.0"
	[ Serilog.Settings.Configuration ] Version="3.1.0"
	[ Serilog.Sinks.Datadog.Logs ] Version="0.3.3"
	[ Serilog.Sinks.Debug ] Version="1.0.1"
	[ Serilog.Sinks.File ] Version="4.1.0"
	[ SerilogAnalyzer ] Version="0.15.0"

Full list you may found in projctect description:  https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetCore/ConsoleApp-NetCore.csproj  

## **ConsoleApp-NetFramework**: .Net Framework 4.5 
Installed packages: 

	[ Serilog ] Version="2.10.0" 
	[ Serilog.Enrichers.Environment ] Version="2.1.3" 
	[ Serilog.Enrichers.Thread] Version="3.1.0"
	[ Serilog.Sinks.Datadog.Logs ] Version="0.3.3"
	[ Serilog.Sinks.File ] Version="4.1.0"
	[ Serilog.Sinks.PeriodicBatching ] Version="2.2.0"
	[ SerilogAnalyzer ] Version="0.15.0"
	[ System.Net.Http ] Version="4.3.4"
	[ Newtonsoft.Json ] Version="11.0.2"

Full list you may found in projctect description:  https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetFramework/ConsoleApp-NetFramework.csproj  

---

# Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'
## Documentation: asking to use 'url' parameter for the LoggerConfiguration

Link: https://docs.datadoghq.com/logs/log_collection/csharp/?tab=serilog#agentless-logging

![Documentation - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20Documentation%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)

## 1/2 Proof it's impossible - NetCore 
Direct link to code snippet on the github: https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetCore/Program.cs#L31  

![Proof - NetFramework - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20NetCore%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)

## 2/2 Proof it's impossible - NetFramework

Direct link to code snippet on the github: https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetFramework/Program.cs#L31

![Proof - NetFramework - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20NetFramework%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)











--- 
# Issue 2 - Selflog is not working  
There are ZERO debug self logs from serilog.  Logs were created in the local file, but **SerilogDebug.txt** is empty. 


Code: 

[NetCore] Direct link to code snippet on the github  
https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetCore/Program.cs#L43

[NetFramework] Direct link to code snippet on the github  
https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetFramework/Program.cs#L43

## Code

	using Serilog;   
	using Serilog.Formatting.Json;   
	using System;   
	using System.Diagnostics;    
	using System.IO;``  

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
	logger.Fatal(message);``   



![](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/Selflog%20is%20not%20working/%5B%20Selflog%20is%20not%20working%20%5D%20%5B%20NetCore%20%5D%20Logs%20results.png?raw=true)

## [ Proof 1/2 - NetCore ] with File sink 

#### Empty console:
![Empty console](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/Selflog%20is%20not%20working/%5B%20Selflog%20is%20not%20working%20%5D%20%5B%20NetCore%20%5D%20console%20output.png?raw=true)

#### Output 

	'ConsoleApp-NetCore.exe' (CoreCLR: DefaultDomain): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Users\Anton Vlasiuk\source\repos\SerilogToDataDog\ConsoleApp-NetCore\bin\Debug\netcoreapp3.1\ConsoleApp-NetCore.dll'. Symbols loaded.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Runtime.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Runtime.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Users\Anton Vlasiuk\source\repos\SerilogToDataDog\ConsoleApp-NetCore\bin\Debug\netcoreapp3.1\Serilog.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.IO.FileSystem.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\netstandard.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Console.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Users\Anton Vlasiuk\source\repos\SerilogToDataDog\ConsoleApp-NetCore\bin\Debug\netcoreapp3.1\Serilog.Sinks.File.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Text.Encoding.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Users\Anton Vlasiuk\source\repos\SerilogToDataDog\ConsoleApp-NetCore\bin\Debug\netcoreapp3.1\Serilog.Enrichers.Thread.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Users\Anton Vlasiuk\source\repos\SerilogToDataDog\ConsoleApp-NetCore\bin\Debug\netcoreapp3.1\Serilog.Enrichers.Environment.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Threading.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Text.Encoding.Extensions.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Collections.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.IO.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.IO.FileSystem.Primitives.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Linq.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
	'ConsoleApp-NetCore.exe' (CoreCLR: clrhost): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Private.Uri.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
The program '[16376] ConsoleApp-NetCore.exe' has exited with code 0 (0x0).

### Logged data 
![](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/Selflog%20is%20not%20working/%5B%20Selflog%20is%20not%20working%20%5D%20%5B%20NetCore%20%5D%20selflog%20+%20logspng.png?raw=true)

#### log.json
100% empty 

#### SerilogDebug.txt

	{"Timestamp":"2020-10-18T18:28:04.9865812+02:00","Level":"Warning","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
	{"Timestamp":"2020-10-18T18:28:04.9960650+02:00","Level":"Information","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
	{"Timestamp":"2020-10-18T18:28:04.9962302+02:00","Level":"Error","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
	{"Timestamp":"2020-10-18T18:28:04.9973622+02:00","Level":"Fatal","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
	
	
## [ Proof 2/2 - NetFramework ] with File sink 

Exact same result as NetCore - logs were created in the file, but **SerilogDebug.txt** is empty 
































### Issue 3 - serilog C# code (NOT the agent.) is not sending logs to the datadog

Code: 

NetCore
https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetCore/Program.cs#L69

NetFramework
https://github.com/AvgustPol/SerilogToDataDog/blob/master/ConsoleApp-NetFramework/Program.cs#L69

I tried added 1 basic DataDog logger 

        public static Logger CreateDataDogLogger()
        {
            return new LoggerConfiguration()
                .WriteTo.DatadogLogs(_apiKey)
                .CreateLogger();
        }

and brute force all combinations of documented settings 

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

as a result - list of 85 loggers, that are sending 4 different level of logs and nothing in out DataDog app. 

on logs page there is still "Get started with logs" message 

