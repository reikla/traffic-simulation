using TrafficSimulation.Common;

namespace TrafficSimulation.TrafficLightControl.WebService
{
  class Program
  {
    static void Main(string[] args)
    {
      var c = new WebServiceController();
      c.Run<TrafficLightControlService>();
    }
  }
}
