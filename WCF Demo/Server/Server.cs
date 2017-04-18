using System.ServiceModel;
using Contracts;

namespace Server
{
  /// <summary>
  ///   Diese Klasse kapselt den WCF Server
  /// </summary>
  internal class Server
  {
    private readonly ServiceHost serviceHost;

    public Server()
    {
      serviceHost = new ServiceHost(typeof(LogService));
      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
      serviceHost.AddServiceEndpoint(typeof(ILogService), binding, Constants.Address);
    }

    /// <summary>
    ///   Start des servers
    /// </summary>
    public void Start()
    {
      serviceHost.Open();
    }

    /// <summary>
    ///   Stop des servers
    /// </summary>
    public void Stop()
    {
      serviceHost.Close();
    }
  }
}