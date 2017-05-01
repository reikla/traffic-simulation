namespace TrafficSimulation.Simulation.Engine.Environment
{
  public class StartNode : Node, IStartNode
  {
    public IVehicle CreateVehicle()
    {
      throw new System.NotImplementedException();
    }

    public StartNode(double x, double y) : base(x, y)
    {
    }
  }
}