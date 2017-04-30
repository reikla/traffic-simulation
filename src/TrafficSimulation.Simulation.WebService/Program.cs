using System;
using System.ServiceModel;
using System.Text;
using NLog;

namespace TrafficSimulation.Simulation.WebService
{
  class Program
  {
    static Logger _logger = LogManager.GetCurrentClassLogger();

    static void Main(string[] args)
    {

      var serviceHost = new ServiceHost(typeof(SimulationService));
      DebugServiceHost(serviceHost);
      serviceHost.Opened += ServiceHost_Opened;
      serviceHost.Faulted += ServiceHost_Faulted;
      serviceHost.UnknownMessageReceived += ServiceHost_UnknownMessageReceived;
      serviceHost.Open();

      Console.WriteLine("Simulation Webservice Started. Press any key to exit.");
      Console.ReadKey();
    }

    private static void DebugServiceHost(ServiceHost serviceHost)
    {
      StringBuilder baseAddressChainer = new StringBuilder();
      baseAddressChainer.Append("BaseAddress: ");

      foreach (var serviceHostBaseAddress in serviceHost.BaseAddresses)
      {
        baseAddressChainer.Append(
          $"[Absolute Uri: {serviceHostBaseAddress.AbsoluteUri} Absolute Path: {serviceHostBaseAddress.AbsolutePath}]");
      }



      _logger.Debug(baseAddressChainer.ToString);
    }

    private static void ServiceHost_Opened(object sender, EventArgs e)
    {
      _logger.Debug("Service Host Opened" + e);
    }

    private static void ServiceHost_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
    {
      _logger.Error("Unknown message received" + e.Message);
    }

    static void Error(string message)
    {
    }

    private static void ServiceHost_Faulted(object sender, EventArgs e)
    {
      _logger.Error("ServiceHost_Faulted " + e);
    }
  }
}
