using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Environment
{
  public class StartNode : Node, IStartNode
  {
    public IVehicle CreateVehicle()
    {
      throw new System.NotImplementedException();
    }
  }
}