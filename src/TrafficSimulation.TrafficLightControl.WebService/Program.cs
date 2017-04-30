using System;
using System.ServiceModel;
using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.TrafficLightControl.WebService
{
  class Program
  {
    static void Main(string[] args)
    {
      var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.Transport);
      var ep = new EndpointAddress("net.pipe://localhost/Simulation/Engine");
      var simulationService = ChannelFactory<ISimulationService>.CreateChannel(binding, ep);


      simulationService.Start();

      Console.WriteLine("Traffic light Control Webservice Started. Press any key to exit.");
      Console.ReadKey();
    }
  }
}
