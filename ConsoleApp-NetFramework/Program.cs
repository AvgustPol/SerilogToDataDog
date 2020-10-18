namespace ConsoleApp_NetFramework
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //// This one is not working.
            //// LoggerConfiguration doesn't have url parameter named 'url'
            //Logger logger1 = CreateLogger1("http-intake.logs.datadoghq.eu", "<API_KEY>");
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