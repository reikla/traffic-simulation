using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Environment
{
  public class StartNode : SimulationBase, IStartNode
  {
    public IVehicle CreateVehicle()
    {
      throw new System.NotImplementedException();
    }
  }
}