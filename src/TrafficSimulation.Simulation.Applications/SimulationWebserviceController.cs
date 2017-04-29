using System.ServiceModel;

namespace TrafficSimulation.Simulation.Applications
{
  class SimulationWebserviceController : ProcessController
  {
    public SimulationWebserviceController() : base(true, 
      "TrafficSimulation.Simulation.WebService",
      @"..\..\..\TrafficSimulation.Simulation.WebService\bin\Debug", null)
    {
    }

  }
}