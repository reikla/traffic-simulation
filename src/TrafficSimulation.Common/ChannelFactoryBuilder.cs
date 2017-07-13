using System.ServiceModel;

namespace TrafficSimulation.Common
{
  /// <summary>
  /// A Helper class to build channel factories
  /// </summary>
  public static class ChannelFactoryBuilder
  {
    /// <summary>
    /// Gets the channel factory.
    /// </summary>
    /// <typeparam name="T">Type of Service</typeparam>
    /// <param name="endpointAdress">The endpoint adress.</param>
    public static ChannelFactory<T> GetChannelFactory<T>(string endpointAdress)
    {
      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
      var ep = new EndpointAddress(endpointAdress);
      return new ChannelFactory<T>(binding, ep);
    }
  }
}