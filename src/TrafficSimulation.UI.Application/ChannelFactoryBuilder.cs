using System.ServiceModel;

namespace TrafficSimulation.UI.Application
{
  internal static class ChannelFactoryBuilder
  {
    public static ChannelFactory<T> GetChannelFactory<T>(string endpointAdress)
    {
      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
      var ep = new EndpointAddress(endpointAdress);
      return new ChannelFactory<T>(binding, ep);
    }
  }
}