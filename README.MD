This solution was created to demonstrate issue with serilog agentless sending logs to the DataDog 

# Projects description:  

**ConsoleApp-NetCore**: .NET CORE 3.1
**ConsoleApp-NetFramework**: .Net Framework 4.5 

---

# Issue 1 - LoggerConfiguration doesn't have url parameter named 'url'
## Documentation: asking to use 'url' parameter for the LoggerConfiguration
![Documentation - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20Documentation%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)

## Proof it's impossible 1/2 - NetCore 
![Proof - NetFramework - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20NetCore%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)

## Proof it's impossible 2/2 - NetFramework 
![Proof - NetFramework - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20NetFramework%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)

--- 
# Issue 2 - Selflog is not working  
There are ZERO debug self logs from serilog. 

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

``'ConsoleApp-NetCore.exe' (CoreCLR: DefaultDomain): Loaded 'C:\Program Files\dotnet\shared\Microsoft.NETCore.App\3.1.7\System.Private.CoreLib.dll'. Skipped loading symbols. Module is optimized and the debugger option 'Just My Code' is enabled.
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

#### Empty log.json
100% empty 

#### SerilogDebug.txt

            {"Timestamp":"2020-10-18T18:28:04.9865812+02:00","Level":"Warning","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
            {"Timestamp":"2020-10-18T18:28:04.9960650+02:00","Level":"Information","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
            {"Timestamp":"2020-10-18T18:28:04.9962302+02:00","Level":"Error","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}
            {"Timestamp":"2020-10-18T18:28:04.9973622+02:00","Level":"Fatal","MessageTemplate":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","RenderedMessage":"Issue 2 [ConsoleApp-NetCore] POC info - 18:28:04","Properties":{"ThreadId":1,"MachineName":"NSX-WKS-002"}}

