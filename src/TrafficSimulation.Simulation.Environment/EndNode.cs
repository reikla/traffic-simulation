using TrafficSimulation.Simulation.Contracts;

namespace TrafficSimulation.Simulation.Environment
{
  public class EndNode : Node, IEndNode
  {
    public void DestroyVehicle(IVehicle vehicle)
    {
      throw new System.NotImplementedException();
    }
  }
}