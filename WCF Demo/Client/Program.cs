using System;
using System.ServiceModel;
using Contracts;

namespace Client
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      Console.WriteLine("Waiting for server. Press button to start.");
      Console.ReadKey();

      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
      var ep = new EndpointAddress("net.pipe://localhost/Simulation/Engine");
      var logService = ChannelFactory<ILogService>.CreateChannel(binding, ep);

      Console.WriteLine("Connected to Log Server.");
      Console.WriteLine("Enter empty line to exit.");

      try
      {
        while (true)
        {
          Console.WriteLine("Enter Log Message: ");
          var message = Console.ReadLine();
          if (string.IsNullOrWhiteSpace(message))
          {
            throw new Exception("Exit");
          }
          logService.Log(message);
          logService.LogAdvanced(new LogMessage {Message = message, TimeContext = new TimeContext {Time = DateTime.Now}});
        }
      }
      catch (Exception ex)
      {
        //exit
      }
    }
  }
}