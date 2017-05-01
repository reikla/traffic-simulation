namespace TrafficSimulation.Simulation.Engine.Environment
{
  public class EndNode : Node, IEndNode
  {
    public void DestroyVehicle(IVehicle vehicle)
    {
      
    }

    public EndNode(double x, double y) : base(x, y)
    {
    }
  }
}