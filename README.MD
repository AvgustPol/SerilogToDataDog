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

## Proof it's impossible 1/2 - NetFramework 
![Proof - NetFramework - LoggerConfiguration doesn't have url parameter named 'url'](https://github.com/AvgustPol/SerilogToDataDog/blob/master/img/%5B%20NetFramework%20%5D%20LoggerConfiguration%20doesn't%20have%20url%20parameter%20named%20'url'.png?raw=true)

--- 


# Issue 2 - DataDog Sink 
