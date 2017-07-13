using System;
using System.ServiceModel;
using System.Text;
using NLog;

namespace TrafficSimulation.Common
{
  /// <summary>
  /// A Controller class for a Webservice
  /// </summary>
  public class WebServiceController
  {
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Runs the Webservice instance.
    /// </summary>
    public void Run<T>()
    {
      var serviceHost = new ServiceHost(typeof(T));
      DebugServiceHost(serviceHost);
      serviceHost.Opened += ServiceHost_Opened;
      serviceHost.Faulted += ServiceHost_Faulted;
      serviceHost.UnknownMessageReceived += ServiceHost_UnknownMessageReceived;
      serviceHost.Open();

      Console.WriteLine($"Webservice '{typeof(T).Name}' started. Press any key to exit.");
      Console.ReadKey();
    }

    private void DebugServiceHost(ServiceHost serviceHost)
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

    private void ServiceHost_Opened(object sender, EventArgs e)
    {
      _logger.Debug("Service Host Opened" + e);
    }

    private void ServiceHost_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
    {
      _logger.Error("Unknown message received" + e.Message);
    }

    private void Error(string message)
    {
    }

    private void ServiceHost_Faulted(object sender, EventArgs e)
    {
      _logger.Error("ServiceHost_Faulted " + e);
    }
  }
}