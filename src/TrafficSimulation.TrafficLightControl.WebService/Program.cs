using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NLog;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.TrafficLightControl.WebService
{
  class Program
  {
    static void Main(string[] args)
    {
      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
      var ep = new EndpointAddress("net.pipe://localhost/Simulation/Engine.svc");
      var logService = ChannelFactory<ISimulationService>.CreateChannel(binding, ep);

      logService.Start();
    }
  }
}
