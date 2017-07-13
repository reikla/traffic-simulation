using TrafficSimulation.Common;

namespace TrafficSimulation.Simulation.WebService
{
  class Program
  {
    static void Main(string[] args)
    {
      var webServiceController = new WebServiceController();
      webServiceController.Run<SimulationService>();
    }
  }
}
