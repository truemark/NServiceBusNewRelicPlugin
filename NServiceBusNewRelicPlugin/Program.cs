using NewRelic.Platform.Sdk;
using System;

namespace NServiceBusNewRelicPlugin
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Runner runner = new Runner();
                runner.Add(new NServiceBusAgentFactory());
                runner.SetupAndRun();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to start the plugin: " + ex.Message);
            }
        }
    }
}
