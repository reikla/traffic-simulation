using System;
using System.ServiceModel;

namespace TrafficSimulation.Logging.ConsoleLogViewer
{
  class Program
  {
    static void Main(string[] args)
    {
      using (var host = new ServiceHost(typeof(LogReceiverServer)))
      {
        host.Open();

        Console.WriteLine("The service is ready. Press [Enter] to stop it.");
        Console.ReadLine();

        // Close the ServiceHost.
        host.Close();
      }
    }
  }
}
